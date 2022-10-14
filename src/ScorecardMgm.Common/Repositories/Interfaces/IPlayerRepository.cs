using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;

namespace ScorecardMgm.Common.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllPlayers(PlayerFilter playerFilter);
        Task<Player> GetPlayer(string playerId);
        Task AddPlayer(Player player);
        Task UpdatePlayer(Player player);
        Task DeletePlayer(string playerId);
    }
}