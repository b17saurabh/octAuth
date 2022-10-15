using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Contracts;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.API.APIroutes;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ScorecardMgm.API.Controllers;

[ApiController]

public class MatchController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly IMapper _mapper;

    public MatchController(IMatchService matchService, IMapper mapper)
    {
        _matchService = matchService;
        _mapper = mapper;
    }

    [HttpGet(Routes.Match.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] MatchFilter filter)
    {
        try
        {
            var matches = await _matchService.GetAllMatchesAsync(filter);
            return Ok(matches);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(Routes.Match.Get)]
    public async Task<IActionResult> Get([FromRoute] string matchId)
    {
        try
        {
            var match = await _matchService.GetMatchAsync(matchId);
            return Ok(match);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(Routes.Match.Create)]
    public async Task<IActionResult> Create([FromBody] Match matchRequest, [FromRoute] string tournamentId)
    {
        try
        {
            var match = _mapper.Map<Models.Match>(matchRequest);
            match.MatchId = Guid.NewGuid().ToString();
            match.TournamentId = tournamentId;
            await _matchService.AddMatchAsync(tournamentId, match);
            return Ok(match);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(Routes.Match.Update)]
    public async Task<IActionResult> Update([FromRoute] string matchId, [FromBody] Match matchRequest)
    {
        try
        {
            var match = _mapper.Map<Models.Match>(matchRequest);
            match.MatchId = matchId;
            await _matchService.UpdateMatchAsync(match);
            return Ok(match);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(Routes.Match.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string matchId)
    {
        try
        {
            await _matchService.DeleteMatchAsync(matchId);
            return Ok(matchId + " deleted");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}