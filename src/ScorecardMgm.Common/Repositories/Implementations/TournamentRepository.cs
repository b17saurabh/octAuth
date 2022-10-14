using Microsoft.EntityFrameworkCore;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Common.Repositories.Implementations
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ScorecardMgmContext _context;

        public TournamentRepository(ScorecardMgmContext context)
        {
            _context = context;
        }

        public async Task AddTournament(Tournament tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament) + " is null");
            }

            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTournament(string tournamentId)
        {
            if (!TournamentExists(tournamentId))
            {
                throw new ArgumentException(nameof(tournamentId) + " tournament doesn't exist");
            }
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Tournament>> GetAllTournaments(TournamentFilter tournamentFilter)
        {
            IQueryable<Tournament> tournament = _context.Tournaments;
            if (tournamentFilter != null)
                tournament = ApplyTournamentFilter(tournamentFilter);
            var tournamentList = await tournament.ToListAsync();
            return tournamentList;
        }

        private IQueryable<Tournament> ApplyTournamentFilter(TournamentFilter tournamentFilter)
        {
            IQueryable<Tournament> tournament = _context.Tournaments;
            if (tournamentFilter != null)
            {
                if (string.IsNullOrEmpty(tournamentFilter.TournamentName) == false)
                    tournament = tournament.Where(c => c.TournamentName.Contains(tournamentFilter.TournamentName));
            }
            return tournament;
        }

        public async Task<Tournament> GetTournament(string tournamentId)
        {
            if (!TournamentExists(tournamentId))
            {
                throw new ArgumentException(nameof(tournamentId) + " Tournament doesn't exist");
            }
            var tournament = await _context.Tournaments.FindAsync(tournamentId);
            return tournament;
        }

        public async Task UpdateTournament(Tournament tournament)
        {
            if (tournament == null)
            {
                throw new ArgumentNullException(nameof(tournament) + " is null");
            }
            if (!TournamentExists(tournament.TournamentId))
            {
                throw new ArgumentException(nameof(tournament.TournamentId) + " tournament doesn't exist");
            }
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();
        }

        private bool TournamentExists(string tournamentId)
        {
            return _context.Tournaments.Any(e => e.TournamentId == tournamentId);
        }
    }
}