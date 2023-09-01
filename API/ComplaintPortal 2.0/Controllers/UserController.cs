using ComplaintPortal.Controllers;
using ComplaintPortalEntities;
using ComplaintPortalServices;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintPortal.Controllers
{
    public class UserController : BaseController<User>
    {
        private readonly UserService _peopleService;
        public UserController(UserService peopleService) : base(peopleService)
        {
            _peopleService = peopleService;
        }
    }
}
