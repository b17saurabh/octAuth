using ScorecardMgm.Auth.Service.Interface;

using Microsoft.AspNetCore.Mvc;
using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Routes;

namespace ScorecardMgm.Auth.Controllers
{
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IAuthServices _userService;

        public UserController(IAuthServices userService)
        {
            _userService = userService;
        }

        [HttpPost(apiRoutes.User.Register)]
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

        [HttpPost(apiRoutes.User.Login)]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            var token = await _userService.Login(request);
            return Ok(token);
        }


    }
}