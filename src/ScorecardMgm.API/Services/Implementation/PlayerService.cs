using AutoMapper;
using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.API.Implementations;

public class PlayerService : IPlayerService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;

    public PlayerService(ITeamRepository teamRepository, IPlayerRepository playerRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _playerRepository = playerRepository;
        _mapper = mapper;
    }

    public Task AddPlayerAsync(Player player)
    {
        var team = _teamRepository.GetTeam(player.TeamId);
        if (team == null)
        {
            throw new Exception("Team not found");
        }
        _playerRepository.AddPlayer(_mapper.Map<ScorecardMgm.Common.Entities.Player>(player));
        return Task.CompletedTask;
    }

    public Task DeletePlayerAsync(string playerId)
    {
        var player = _playerRepository.GetPlayer(playerId);
        if (player == null)
        {
            throw new Exception("Player not found");
        }
        _playerRepository.DeletePlayer(playerId);
        return Task.CompletedTask;
    }

    public Task<List<Player>> GetAllPlayersAsync(PlayerFilter playerFilter)
    {
        return Task.FromResult(_mapper.Map<List<Player>>(_playerRepository.GetAllPlayers(_mapper.Map<ScorecardMgm.Common.Filters.PlayerFilter>(playerFilter))));
    }

    public Task<Player> GetPlayerAsync(string playerId)
    {
        var player = _playerRepository.GetPlayer(playerId);
        if (player == null)
        {
            throw new Exception("Player not found");
        }
        return Task.FromResult(_mapper.Map<Player>(player));
    }

    public Task UpdatePlayerAsync(Player player)
    {
        var playerToUpdate = _playerRepository.GetPlayer(player.PlayerId);
        if (playerToUpdate == null)
        {
            throw new Exception("Player not found");
        }
        _playerRepository.UpdatePlayer(_mapper.Map<ScorecardMgm.Common.Entities.Player>(player));
        return Task.CompletedTask;
    }
}
