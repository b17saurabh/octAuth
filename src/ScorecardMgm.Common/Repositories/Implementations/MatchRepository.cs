using Microsoft.EntityFrameworkCore;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Common.Repositories.Implementations
{
    public class MatchRepository : IMatchRepository
    {
        private readonly ScorecardMgmContext _context;

        public MatchRepository(ScorecardMgmContext context)
        {
            _context = context;
        }

        public async Task AddMatch(Match match)
        {
            try
            {
                if (match == null)
                {
                    throw new ArgumentNullException(nameof(match) + " is null");
                }

                await _context.Matches.AddAsync(match);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task DeleteMatch(string matchId)
        {
            if (!MatchExists(matchId))
            {
                throw new ArgumentException(nameof(matchId) + " must be greater than 0");
            }
            var match = await _context.Matches.FindAsync(matchId);
            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Match>> GetAllMatches(MatchFilter matchFilter)
        {
            IQueryable<Match> match = _context.Matches;
            if (matchFilter != null)
                match = ApplyMatchFilter(matchFilter);
            var matchList = await match.ToListAsync();
            return matchList;
        }

        private IQueryable<Match> ApplyMatchFilter(MatchFilter matchFilter)
        {
            IQueryable<Match> match = _context.Matches;
            if (matchFilter != null)
            {
                if (string.IsNullOrEmpty(matchFilter.MatchName) == false)
                    match = match.Where(c => c.MatchName.Contains(matchFilter.MatchName));
            }
            return match;
        }

        public async Task<Match> GetMatch(string matchId)
        {
            // try
            // {
            if (!MatchExists(matchId))
            {
                throw new ArgumentException(nameof(matchId) + " match does not exist");
            }
            var match = await _context.Matches.FindAsync(matchId);
            return match;
            // }
            // catch 
            // { 
            //     throw new ArgumentException(nameof(matchId) + " not found"); 
            // }
        }

        public async Task UpdateMatch(Match _match)
        {
            if (MatchExists(_match.MatchId))
            {
                var match = await _context.Matches.FindAsync(_match.MatchId);
                _context.Entry(match).CurrentValues.SetValues(_match);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException(nameof(_match.MatchId) + " match does not exist");
            }
        }

        public bool MatchExists(string matchId)
        {
            return _context.Matches.Any(m => m.MatchId == matchId);
        }
    }
}