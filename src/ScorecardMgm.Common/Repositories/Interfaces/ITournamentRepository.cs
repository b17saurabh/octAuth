using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;

namespace ScorecardMgm.Common.Repositories.Interfaces
{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> GetAllTournaments(TournamentFilter tournamentFilter);
        Task<Tournament> GetTournament(string tournamentId);
        Task AddTournament(Tournament tournament);
        Task UpdateTournament(Tournament tournament);
        Task DeleteTournament(string tournamentId);
    }
}