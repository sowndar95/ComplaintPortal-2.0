using ComplaintPortal.Controllers;
using ComplaintPortalEntities;
using ComplaintPortalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace ComplaintPortal.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthenticationController : BaseLogController
    {
        private readonly AuthenticationService _authenticationService;
        public AuthenticationController(AuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AuthModel>> Login(LoginModel login)
        {
            var response = await _authenticationService.Login(login);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(Tokens tokens)
        {
            var response = await _authenticationService.RefreshToken(tokens);
            return Ok(response);
        }
    }
}
