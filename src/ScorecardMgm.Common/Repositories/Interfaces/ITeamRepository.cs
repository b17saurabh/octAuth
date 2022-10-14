using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;

namespace ScorecardMgm.Common.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> GetAllTeams(TeamFilter teamFilter);
        Task<Team> GetTeam(string teamId);
        Task AddTeam(Team team);
        Task UpdateTeam(Team team);
        Task DeleteTeam(string teamId);
    }
}