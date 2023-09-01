using ComplaintPortalEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalServices
{
    public class AuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AuthTokenService _authTokenService;
        private readonly UserService _userService;
        private readonly MenuService _menuService;
        private readonly ApplicationSettings _applicationSettings;
        private readonly RoleService _roleService;
        public AuthenticationService( UserManager<ApplicationUser> userManager, AuthTokenService authTokenService, UserService userService, ApplicationSettings applicationSettings, MenuService menuService, RoleService roleService)
        {
            _userManager = userManager;
            _authTokenService = authTokenService;
            _userService = userService;
            _applicationSettings = applicationSettings;
            _menuService = menuService;
            _roleService = roleService;
        }


        public async Task<AuthModel> Login(LoginModel login)
        {
            //Get the User
            var applicationUser = await _userManager.FindByNameAsync(login.UserName) ?? throw new UnauthorizedAccessException("User name incorrect. Please try again.");

            var authenticationModel = new AuthModel
            {
                UserName = applicationUser.Email,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                UserId = applicationUser.Id,
                Menus = new List<Menu>()
            };

            authenticationModel.Token = _authTokenService.GenerateAccessToken(authenticationModel);
            authenticationModel.RefreshToken = _authTokenService.CreateRefreshToken();

            applicationUser.RefreshToken = authenticationModel.RefreshToken;
            applicationUser.RefreshTokenExpiryDate = DateTime.Now.AddDays(_applicationSettings.JwtSettings.RefreshTokenExpirydays);

            //Get the Roles
            var userRoleNames = await _userManager.GetRolesAsync(applicationUser) ?? Array.Empty<string>();
            var userRoles = _roleService.GetActiveRoles(applicationUser.Id).Where(r => userRoleNames.Contains(r.Name!));

            //Get the Permission for the user
            var userPermissions = Permissions.None;
            foreach (var role in userRoles)
                userPermissions |= role.Permissions;
            var userpermission = (int)userPermissions;

            var menus = new List<Menu>();
            var systemMenus = await _menuService.GetAll();
            foreach (var menu in systemMenus)
            {
                if ((menu.Permission & userPermissions) != 0 || menu.Permission == Permissions.All)
                {
                    menus = systemMenus.Where(m => ((m.Permission & userPermissions) != 0 || m.Permission == Permissions.All)).ToList();
                    authenticationModel.Menus.Add(menu);
                }
            }

            return authenticationModel;
        }

        public async Task<Tokens> RefreshToken(Tokens tokens)
        {
            var principal = _authTokenService.ValidateToken(tokens.Token);
            if (principal == null)
            {
                throw new KeyNotFoundException("Invalid access token or refresh token");
            }

            var claim = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Name);
            if (claim == null)
            {
                throw new KeyNotFoundException("Invalid access token or refresh token");
            }
            var username = claim.Value;
            if (string.IsNullOrEmpty(username))
            {
                throw new KeyNotFoundException("Invalid access token or refresh token");
            }
            var applicationUser = await _userManager.FindByNameAsync(username);
            if (applicationUser == null
                || applicationUser.RefreshToken != tokens.RefreshToken
                || applicationUser.RefreshTokenExpiryDate <= DateTime.Now)
            {
                throw new KeyNotFoundException("Invalid access token or refresh token");
            }
            tokens.Token = _authTokenService.CreateToken(principal.Claims.ToList());
            tokens.RefreshToken = _authTokenService.CreateRefreshToken();

            applicationUser.RefreshToken = tokens.RefreshToken;
            var result = _userManager.UpdateAsync(applicationUser);
            return tokens;
        }
    }
}
