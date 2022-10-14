using Microsoft.EntityFrameworkCore;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Common.Repositories.Implementations
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ScorecardMgmContext _context;

        public PlayerRepository(ScorecardMgmContext context)
        {
            _context = context;
        }

        public async Task AddPlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player) + " is null");
            }

            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayer(string playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new ArgumentException(nameof(playerId) + " player doesn't exist");
            }
            var player = await _context.Players.FindAsync(playerId);
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Player>> GetAllPlayers(PlayerFilter playerFilter)
        {
            IQueryable<Player> player = _context.Players;
            if (playerFilter != null)
                player = ApplyPlayerFilter(playerFilter);
            var playerList = await player.ToListAsync();
            return playerList;
        }

        private IQueryable<Player> ApplyPlayerFilter(PlayerFilter playerFilter)
        {
            IQueryable<Player> player = _context.Players;
            if (playerFilter != null)
            {
                if (string.IsNullOrEmpty(playerFilter.PlayerName) == false)
                    player = player.Where(c => c.PlayerName.Contains(playerFilter.PlayerName));
            }
            return player;
        }

        public async Task<Player> GetPlayer(string playerId)
        {
            if (!PlayerExists(playerId))
            {
                throw new ArgumentException(nameof(playerId) + " player doesn't exist");
            }
            var player = await _context.Players.FindAsync(playerId);
            return player;
        }

        public async Task UpdatePlayer(Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player) + " is null");
            }
            if (!PlayerExists(player.PlayerId))
            {
                throw new ArgumentException(nameof(player.PlayerId) + " player doesn't exist");
            }
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        private bool PlayerExists(string playerId)
        {
            return _context.Players.Any(e => e.PlayerId == playerId);
        }
    }
}