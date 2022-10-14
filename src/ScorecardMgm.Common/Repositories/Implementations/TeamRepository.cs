using Microsoft.EntityFrameworkCore;
using ScorecardMgm.Common.Entities;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.Common.Repositories.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ScorecardMgmContext _context;

        public TeamRepository(ScorecardMgmContext context)
        {
            _context = context;
        }

        public async Task AddTeam(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team) + " is null");
            }

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeam(string teamId)
        {
            // if (!TeamExists(teamId))
            // {
            //     throw new ArgumentException(nameof(teamId) + " team doesn't exist");
            // }
            var team = await GetTeam(teamId);
            if (team == null)
            {
                throw new KeyNotFoundException("Team not found");
            }
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Team>> GetAllTeams(TeamFilter teamFilter)
        {
            IQueryable<Team> team = _context.Teams;
            if (teamFilter != null)
                team = ApplyTeamFilter(teamFilter);
            var teamList = await team.ToListAsync();
            return teamList;
        }

        private IQueryable<Team> ApplyTeamFilter(TeamFilter teamFilter)
        {
            IQueryable<Team> team = _context.Teams;
            if (teamFilter != null)
            {
                if (string.IsNullOrEmpty(teamFilter.TeamName) == false)
                    team = team.Where(c => c.TeamName.Contains(teamFilter.TeamName));
            }
            return team;
        }

        public async Task<Team> GetTeam(string teamId)
        {
            if (!TeamExists(teamId))
            {
                throw new ArgumentException(nameof(teamId) + " team doesn't exist");
            }
            var team = await _context.Teams.FindAsync(teamId);
            return team;
        }

        public async Task UpdateTeam(Team team)
        {
            if (team == null)
            {
                throw new ArgumentNullException(nameof(team) + " is null");
            }
            // if (!TeamExists(team.TeamId))
            // {
            //     throw new ArgumentException(nameof(team.TeamId) + " team doesn't exist");
            // }
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        private bool TeamExists(string teamId)
        {
            return _context.Teams.Any(e => e.TeamId == teamId);
        }
    }
}