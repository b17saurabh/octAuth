using Microsoft.AspNetCore.Mvc;
using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Routes;
using ScorecardMgm.Auth.Services.Interface;

namespace ScorecardMgm.Auth.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthServices _authService;

    public AuthController(IAuthServices authService)
    {
        _authService = authService;
    }

    [HttpPost(apiRoutes.Auth.Login)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        try
        {
            var token = await _authService.Login(request);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}