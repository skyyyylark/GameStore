using Abstractions.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Security.Claims;
using System.Text;

namespace test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<int> Register([FromBody] RegisterModel model)
        {
            return await _userService.Register(model);
        }

        [HttpPost("login")]
        public async Task<int> Login([FromBody] LoginModel model)
        {
            return await _userService.Login(model);
        }

        [HttpPost("logout")]
        public async Task<int> Logout()
        {
            return await _userService.Logout(); 
        }
    }
}
