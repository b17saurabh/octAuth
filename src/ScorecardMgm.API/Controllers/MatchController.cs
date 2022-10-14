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
        var matches = await _matchService.GetAllMatchesAsync(filter);
        return Ok(matches);
    }

    [HttpGet(Routes.Match.Get)]
    public async Task<IActionResult> Get([FromRoute] string matchId)
    {
        var match = await _matchService.GetMatchAsync(matchId);
        return Ok(match);
    }

    [HttpPost(Routes.Match.Create)]
    public async Task<IActionResult> Create([FromBody] Match matchRequest, [FromRoute] string tournamentId)
    {
        var match = _mapper.Map<Models.Match>(matchRequest);
        match.MatchId = Guid.NewGuid().ToString();
        await _matchService.AddMatchAsync(tournamentId, match);
        return Ok(match);
    }

    [HttpPut(Routes.Match.Update)]
    public async Task<IActionResult> Update([FromRoute] string matchId, [FromBody] Match matchRequest)
    {
        var match = _mapper.Map<Models.Match>(matchRequest);
        match.MatchId = matchId;
        await _matchService.UpdateMatchAsync(match);
        return Ok(match);
    }

    [HttpDelete(Routes.Match.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string matchId)
    {
        await _matchService.DeleteMatchAsync(matchId);
        return NoContent();
    }
}