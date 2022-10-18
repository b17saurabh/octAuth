using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Contracts;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.API.APIroutes;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ScorecardMgm.API.Controllers;

[ApiController]

public class TournamentController : ControllerBase
{
    private readonly ITournamentService _tournamentService;
    private readonly IMapper _mapper;

    public TournamentController(ITournamentService tournamentService, IMapper mapper)
    {
        _tournamentService = tournamentService;
        _mapper = mapper;
    }

    [HttpGet(Routes.Tournament.GetAll)]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] TournamentFilter filter)
    {
        try
        {
            var tournaments = await _tournamentService.GetAllTournamentsAsync(filter);
            return Ok(tournaments);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(Routes.Tournament.Get)]
    public async Task<IActionResult> Get([FromRoute] string tournamentId)
    {
        try
        {
            var tournament = await _tournamentService.GetTournamentAsync(tournamentId);
            return Ok(tournament);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost(Routes.Tournament.Create)]
    public async Task<IActionResult> Create([FromBody] Tournament tournamentRequest)
    {
        try
        {
            var tournament = _mapper.Map<Models.Tournament>(tournamentRequest);
            tournament.TournamentId = Guid.NewGuid().ToString();
            await _tournamentService.AddTournamentAsync(tournament);
            return Ok(tournament);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(Routes.Tournament.Update)]
    public async Task<IActionResult> Update([FromRoute] string tournamentId, [FromBody] Tournament tournamentRequest)
    {
        try
        {
            var tournament = _mapper.Map<Models.Tournament>(tournamentRequest);
            tournament.TournamentId = tournamentId;
            await _tournamentService.UpdateTournamentAsync(tournament);
            return Ok(tournament);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(Routes.Tournament.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string tournamentId)
    {
        try
        {
            await _tournamentService.DeleteTournamentAsync(tournamentId);
            return Ok(tournamentId);
        }
        catch
        {
            return NotFound();
        }
    }
}