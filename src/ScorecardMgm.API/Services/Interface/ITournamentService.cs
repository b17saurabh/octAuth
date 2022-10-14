using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;

namespace ScorecardMgm.API.Interfaces;

public interface ITournamentService
{
    Task<List<Tournament>> GetAllTournamentsAsync(TournamentFilter tournamentFilter);
    Task<Tournament> GetTournamentAsync(string tournamentId);
    Task AddTournamentAsync(Tournament tournament);
    Task UpdateTournamentAsync(Tournament tournament);
    Task DeleteTournamentAsync(string tournamentId);
}