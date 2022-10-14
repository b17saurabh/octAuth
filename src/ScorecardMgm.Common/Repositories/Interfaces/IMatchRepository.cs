using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;


namespace ScorecardMgm.Common.Repositories.Interfaces
{
    public interface IMatchRepository
    {
        Task<List<Match>> GetAllMatches(MatchFilter matchFilter);
        Task<Match> GetMatch(string matchId);
        Task AddMatch(Match match);
        Task UpdateMatch(Match match);
        Task DeleteMatch(string matchId);
    }
}