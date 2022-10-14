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
        var player = await _playerService.GetPlayerAsync(playerid);
        return Ok(player);
    }

    [HttpGet(Routes.Player.GetAll)]
    public async Task<IActionResult> GetAllPlayers([FromQuery] PlayerFilter filter)
    {
        var players = await _playerService.GetAllPlayersAsync(filter);
        return Ok(players);
    }

    [HttpPost(Routes.Player.Create)]
    public async Task<IActionResult> CreatePlayer([FromBody] Player playerDto)
    {
        var playerToBeAdded = _mapper.Map<Models.Player>(playerDto);
        playerToBeAdded.PlayerId = Guid.NewGuid().ToString();
        await _playerService.AddPlayerAsync(playerToBeAdded);
        return Ok(playerToBeAdded);
    }

    [HttpPut(Routes.Player.Update)]
    public async Task<IActionResult> UpdatePlayer([FromBody] Player playerDto)
    {
        var playerToBeUpdated = _mapper.Map<Models.Player>(playerDto);
        await _playerService.UpdatePlayerAsync(playerToBeUpdated);
        return Ok(playerDto);
    }

    [HttpDelete(Routes.Player.Delete)]
    public async Task<IActionResult> DeletePlayer([FromRoute] string playerid)
    {
        await _playerService.DeletePlayerAsync(playerid);
        return Ok();
    }
}