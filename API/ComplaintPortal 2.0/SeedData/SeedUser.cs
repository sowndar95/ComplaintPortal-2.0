using ComplaintPortalEntities;
using ComplaintPortalServices;
using Microsoft.AspNetCore.Identity;

namespace ComplaintPortal_2._0.SeedData
{
    public class SeedUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public SeedUser(UserManager<ApplicationUser> userManager, UserService userService, RoleManager<ApplicationRole> roleManager) 
        {
            _userManager = userManager;
            _userService = userService;
            _roleManager = roleManager;
        }

        public async Task RunAsync()
        {
            await _roleManager.CreateAsync(
            new ApplicationRole
            {
                Name = "User",
                NormalizedName = "User",
                Permissions = Permissions.All,
                IsActive = true,
            });

            var adminRole = await _roleManager.FindByNameAsync("User");

            //ApplicationUser newUser = new()
            //{
            //    Email = "admin@gmail.com",
            //    FirstName = "Admin",
            //    LastName = "User",
            //    UserName = "admin@gmail.com",
            //    Roles = new List<Guid> { adminRole.Id }
            //};

            User user = new()
            {
                Email = "admin@gmail.com",
                FirstName = "Admin",
                LastName = "User",
                FullName = "Admin" + " " + "User",
                Role = adminRole.Id
            };
            await _userService.Insert(user);
        }
    }
}
