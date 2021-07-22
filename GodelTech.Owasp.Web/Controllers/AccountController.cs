using GodelTech.Owasp.Web.Models;
using GodelTech.Owasp.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GodelTech.Owasp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost("/authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
    }
};