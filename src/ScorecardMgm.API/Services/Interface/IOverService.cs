using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;


namespace ScorecardMgm.API.Interfaces;

public interface IOverService
{
    Task<List<Over>> GetAllOversAsync(OverFilter overFilter);
    Task<Over> GetOverAsync(string overId);
    Task<Over> AddOverAsync(string matchId, Over over);
    Task<Over> UpdateOverAsync(string OverId, Over over);
    Task DeleteOverAsync(string overId);
}