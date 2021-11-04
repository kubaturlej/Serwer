using Football.API.DTOs;
using Football.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Football.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult RegisterUser(RegisterDto dto)
        {
            _userService.RegisterUser(dto);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<UserDto> Login(LoginDto dto)
        {
            var user = _userService.LoginUser(dto);
            return Ok(user);
        }

        [Authorize]
        [HttpPost("{id}/delete")]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public ActionResult<UserDto> GetUserAfterClientReload()
        {
            var user = _userService.GetUser(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
            return Ok(user);
        }
    }
}
