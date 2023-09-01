using ComplaintPortalEntities;
using ComplaintPortalServices;
using Microsoft.AspNetCore.Identity;
using ComplaintPortalEntities.Authentication;
using System.Net.Mail;

namespace ComplaintPortalServices
{
    public class UserService : BaseService<User>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserService(ApplicationSettings settings, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager): base(settings) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override async Task<User> Insert(User userProfile)
        {
            bool isAdd = userProfile.Id == Guid.Empty;

            var applicationUser = await _userManager.FindByEmailAsync(userProfile.Email); 

            applicationUser ??= new ApplicationUser
            {
                Email = userProfile.Email,
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                UserName = userProfile.Email,
                Roles = new List<Guid> { userProfile.Role }
            };            

            if (isAdd)
            {
                await _userManager.CreateAsync(applicationUser, "User123@@");
            }
            else
            {
                await _userManager.UpdateAsync(applicationUser);
            }

            userProfile.Id = applicationUser.Id;
            var result = await base.Insert(userProfile);

            var userPermission = Permissions.User;
            var userRole = "User";
            var applicationRole = await _roleManager.CreateAsync(
                new ApplicationRole
                {
                    Name = userRole,
                    NormalizedName = userRole,
                    Permissions = userPermission,
                    IsActive = true
                });

            return result;
        }
    }
}