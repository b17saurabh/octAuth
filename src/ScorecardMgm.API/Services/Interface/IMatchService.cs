using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;



namespace ScorecardMgm.API.Interfaces
{
    public interface IMatchService
    {
        Task<List<Match>> GetAllMatchesAsync(MatchFilter matchFilter);
        Task<Match> GetMatchAsync(string matchId);
        Task AddMatchAsync(string tournamentId, Match match);
        Task UpdateMatchAsync(Match match);
        Task DeleteMatchAsync(string matchId);
    }
}