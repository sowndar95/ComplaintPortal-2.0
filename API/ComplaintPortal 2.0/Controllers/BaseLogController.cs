using ComplaintPortal.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseLogController : ControllerBase
    {
        protected ILogger Logger => HttpContext.RequestServices.GetService<ILogger<BaseLogController>>();

    }
}
