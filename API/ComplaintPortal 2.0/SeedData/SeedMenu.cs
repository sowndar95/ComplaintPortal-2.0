using ComplaintPortalEntities;
using ComplaintPortalServices;

namespace ComplaintPortal_2._0.SeedData
{
    public class SeedMenu
    {
        private readonly MenuService _menuService;

        public SeedMenu(MenuService menuService) 
        {
            _menuService = menuService;
        }

        public async Task RunAsync()
        {
            await _menuService.DeleteAllMenu();

            #region User
            Menu userDashboard = new Menu
            {
                MenuName = "DashBoard",
                MenuDescription = "DashBoard",
                CssClass = "home-fill",
                Permission = Permissions.User,
            };
            await _menuService.Insert(userDashboard);

            Menu complaint = new Menu
            {
                MenuName = "Complaints",
                MenuDescription = "Complaints",
                MenuUrl = "/OrganizationList",
                Permission = Permissions.User,
            };
            await _menuService.Insert(complaint);
            #endregion
        }
    }
}
