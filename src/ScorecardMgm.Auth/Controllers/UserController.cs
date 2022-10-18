using ScorecardMgm.Auth.Services.Interface;

using Microsoft.AspNetCore.Mvc;
using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Routes;
using Microsoft.AspNetCore.Authorization;

namespace ScorecardMgm.Auth.Controllers
{
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserServices _userService;

        public UserController(IUserServices userService)
        {
            _userService = userService;
        }

        [HttpPost(apiRoutes.User.Register)]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto request)
        {
            try
            {
                await _userService.Register(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpDelete(apiRoutes.User.DeleteUser)]
        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(apiRoutes.User.GetUserById)]
        [Authorize]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(apiRoutes.User.GetAllUsers)]
        [Authorize]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut(apiRoutes.User.UpdateUser)]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, UserUpdate request)
        {
            try
            {
                await _userService.UpdateUser(userId, request);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}