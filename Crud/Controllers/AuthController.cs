using Crud.DTO;
using Crud.Models;
using Crud.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _user;
        public AuthController(IUserService user)
        {
            _user = user;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Create([FromBody] RegisterDTO user)
        {
            var result = _user.Create(user);
            return result;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO userLogin)
        {
            var result = _user.Authentication(userLogin);
            return result;
        }
    }
}
