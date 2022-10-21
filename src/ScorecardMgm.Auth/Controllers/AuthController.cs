using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ScorecardMgm.Auth.Contract;
using ScorecardMgm.Auth.Routes;
using ScorecardMgm.Auth.Services.Interface;

namespace ScorecardMgm.Auth.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthServices _authService;
    // protected string token { get; set; }

    public AuthController(IAuthServices authService)
    {
        _authService = authService;
    }

    // public override void OnActionExecuting(ActionExecutingContext context)
    // {
    //     var authHeader = context.HttpContext.Request.Headers["Authorization"];
    //     var HeaderStringList = authHeader.ToString().Split(" ").ToList();
    //     if (HeaderStringList.Count == 2)
    //     {
    //         var token = HeaderStringList[1];
    //         var result = _authService.ValidateToken(token);
    //     }
    //     else
    //     {
    //         context.Result = new UnauthorizedResult();
    //     }
    //     base.OnActionExecuting(context);
    // }

    [HttpPost(apiRoutes.Auth.Login)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        try
        {
            var token = await _authService.Login(request);
            return Ok(token);
            // context.Response.ContentType = "application/json";
            // context.Response.StatusCode = (int)HttpStatusCode.OK;

            // return Ok(context.Response.WriteAsync(JsonConvert.SerializeObject(new { token = token })));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(apiRoutes.Auth.ValidateToken)]
    public async Task<IActionResult> ValidateToken()
    {
        try
        {
            var result = await _authService.ValidateToken(HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}