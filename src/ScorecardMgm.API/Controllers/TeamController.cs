using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Contracts;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.API.APIroutes;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ScorecardMgm.API.Controllers;

[ApiController]

public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;
    private readonly IMapper _mapper;

    public TeamController(ITeamService teamService, IMapper mapper)
    {
        _teamService = teamService;
        _mapper = mapper;
    }

    [HttpGet(Routes.Team.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] TeamFilter filter)
    {
        try
        {
            var teams = await _teamService.GetAllTeamsAsync(filter);
            return Ok(teams);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(Routes.Team.Get)]
    public async Task<IActionResult> Get([FromRoute] string teamId)
    {
        try
        {
            var team = await _teamService.GetTeamAsync(teamId);
            return Ok(team);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(Routes.Team.Create)]
    public async Task<IActionResult> Create([FromBody] Team teamRequest)
    {
        try
        {
            var team = _mapper.Map<Models.Team>(teamRequest);
            team.TeamId = Guid.NewGuid().ToString();
            await _teamService.AddTeamAsync(team);
            return Ok(team);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(Routes.Team.Update)]
    public async Task<IActionResult> Update([FromRoute] string teamId, [FromBody] Team teamRequest)
    {
        try
        {
            var team = _mapper.Map<Models.Team>(teamRequest);
            team.TeamId = teamId;
            await _teamService.UpdateTeamAsync(team);
            return Ok(team);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(Routes.Team.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string teamId)
    {
        try
        {
            await _teamService.DeleteTeamAsync(teamId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}