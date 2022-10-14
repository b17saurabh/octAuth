using Microsoft.EntityFrameworkCore;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Common.Repositories.Implementations
{
    public class OverRepository : IOverRepository
    {
        private readonly ScorecardMgmContext _context;

        public OverRepository(ScorecardMgmContext context)
        {
            _context = context;
        }

        public async Task AddOver(Over over)
        {
            if (over == null)
            {
                throw new ArgumentNullException(nameof(over) + " is null");
            }

            await _context.Overs.AddAsync(over);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOver(string overId)
        {
            if (!OverExists(overId))
            {
                throw new ArgumentException(nameof(overId) + " over doesn't exist");
            }
            var over = await _context.Overs.FindAsync(overId);
            _context.Overs.Remove(over);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Over>> GetAllOversFromAMatch(OverFilter overFilter)
        {
            if (!MatchExists(overFilter.MatchId))
            {
                throw new ArgumentException(nameof(overFilter.MatchId) + " match id does not exist");
            }
            var overList = await _context.Overs.Where(c => c.MatchId == overFilter.MatchId).ToListAsync();
            return overList;
        }


        public async Task<Over> GetOver(string overId)
        {
            if (!OverExists(overId))
            {
                throw new ArgumentException(nameof(overId) + " doestn't exist");
            }
            var over = await _context.Overs.FindAsync(overId);
            return over;
        }

        public async Task UpdateOver(Over over)
        {
            if (OverExists(over.OverId))
            {
                var _over = await _context.Overs.FindAsync(over.OverId);
                _context.Entry(_over).CurrentValues.SetValues(over);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException(nameof(over.MatchId) + " over does not exist");
            }
        }

        private bool OverExists(string overId)
        {
            return _context.Overs.Any(e => e.OverId == overId);
        }
        public bool MatchExists(string matchId)
        {
            return _context.Matches.Any(m => m.MatchId == matchId);
        }
    }
}