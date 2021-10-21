using Football.API.DTOs;
using Football.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Football.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser(RegisterDto dto)
        {
            _userService.RegisterUser(dto);

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login(LoginDto dto)
        {
            string token = _userService.LoginUser(dto);
            return Ok(token);
        }
    }
}
