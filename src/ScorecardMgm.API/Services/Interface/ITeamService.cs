using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;


namespace ScorecardMgm.API.Interfaces;

public interface ITeamService
{
    Task<List<Team>> GetAllTeamsAsync(TeamFilter teamFilter);
    Task<Team> GetTeamAsync(string teamId);
    Task AddTeamAsync(Team team);
    Task UpdateTeamAsync(Team team);
    Task DeleteTeamAsync(string teamId);
}