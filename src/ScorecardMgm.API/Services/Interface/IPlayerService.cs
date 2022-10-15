using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;


namespace ScorecardMgm.API.Interfaces;

public interface IPlayerService
{
    Task<List<Player>> GetAllPlayersAsync(PlayerFilter playerFilter);
    Task<Player> GetPlayerAsync(string playerId);
    Task<Player> AddPlayerAsync(Player player);
    Task UpdatePlayerAsync(Player player);
    Task DeletePlayerAsync(string playerId);
}