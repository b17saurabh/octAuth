using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Contracts;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.API.APIroutes;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ScorecardMgm.API.Controllers;

[ApiController]

public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly IMapper _mapper;

    public PlayerController(IPlayerService playerService, IMapper mapper)
    {
        _playerService = playerService;
        _mapper = mapper;
    }

    [HttpGet(Routes.Player.Get)]
    public async Task<IActionResult> GetPlayerById([FromRoute] string playerid)
    {
        try
        {
            var player = await _playerService.GetPlayerAsync(playerid);
            return Ok(player);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(Routes.Player.GetAll)]
    public async Task<IActionResult> GetAllPlayers([FromQuery] PlayerFilter filter)
    {
        try
        {
            var players = await _playerService.GetAllPlayersAsync(filter);
            return Ok(players);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(Routes.Player.Create)]
    public async Task<IActionResult> CreatePlayer([FromRoute] string teamId, [FromBody] Player playerDto)
    {
        try
        {
            var playerToBeAdded = _mapper.Map<Models.Player>(playerDto);
            playerToBeAdded.TeamId = teamId;
            playerToBeAdded.PlayerId = Guid.NewGuid().ToString();
            await _playerService.AddPlayerAsync(playerToBeAdded);
            return Ok(playerToBeAdded);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(Routes.Player.Update)]
    public async Task<IActionResult> UpdatePlayer([FromRoute] string playerId, [FromBody] Player playerDto)
    {
        try
        {
            var playerToBeUpdated = _mapper.Map<Models.Player>(playerDto);
            playerToBeUpdated.PlayerId = playerId;
            // playerToBeUpdated.TeamId = teamId;
            await _playerService.UpdatePlayerAsync(playerToBeUpdated);
            return Ok(playerDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(Routes.Player.Delete)]
    public async Task<IActionResult> DeletePlayer([FromRoute] string playerid)
    {
        try
        {
            await _playerService.DeletePlayerAsync(playerid);
            return Ok(playerid);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}