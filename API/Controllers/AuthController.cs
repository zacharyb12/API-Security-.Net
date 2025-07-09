using BLL.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Auth;
using Models.UserModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service; 
        public AuthController(IAuthService service)
        {
            _service = service;
        }


        [HttpPost("Register")]
        public async  Task<IActionResult> Register(AddUser user)
        {
            string token = await _service.Register(user);

            return Ok(token);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginForm login)
        {
            string token = await _service.Login(login);

            return Ok(token);
        }
}
}
