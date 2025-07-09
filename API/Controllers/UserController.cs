using BLL.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models.UserModel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service; 
        }

        #region GetAll
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<UserFull> response = await _userService.GetAll();

            return Ok(response);
        }
        #endregion

        #region GetById
        [Authorize]
        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _userService.GetById(id);

            return Ok(response);
        }
        #endregion

        #region
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUser user)
        {
            var response = await _userService.Update(user);

            return Ok(response);

        }
        #endregion

        #region DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _userService.Delete(id);

            return Ok(response);
        }
        #endregion

    }
}
