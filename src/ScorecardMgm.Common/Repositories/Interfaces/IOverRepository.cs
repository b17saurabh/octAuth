using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;
namespace ScorecardMgm.Common.Repositories.Interfaces
{
    public interface IOverRepository
    {
        Task<List<Over>> GetAllOversFromAMatch(OverFilter overFilter);
        Task<Over> GetOver(string overId);
        Task AddOver(Over over);
        Task UpdateOver(Over over);
        Task DeleteOver(string overId);

    }
}