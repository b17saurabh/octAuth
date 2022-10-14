using AutoMapper;
using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.API.Implementations;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IMapper _mapper;

    public TeamService(ITeamRepository teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }
    public async Task AddTeamAsync(Team team)
    {
        await _teamRepository.AddTeam(_mapper.Map<ScorecardMgm.Common.Entities.Team>(team));
    }

    public async Task DeleteTeamAsync(string teamId)
    {
        // var team = _teamRepository.GetTeam(teamId);
        // if (team == null)
        // {
        //     throw new KeyNotFoundException("Team not found");
        // }
        await _teamRepository.DeleteTeam(teamId);
    }

    public async Task<List<Team>> GetAllTeamsAsync(TeamFilter teamFilter)
    {
        return _mapper.Map<List<Team>>(await _teamRepository.GetAllTeams(_mapper.Map<ScorecardMgm.Common.Filters.TeamFilter>(teamFilter)));
    }

    public async Task<Team> GetTeamAsync(string teamId)
    {
        var team = await _teamRepository.GetTeam(teamId);
        if (team == null)
        {
            throw new KeyNotFoundException("Team not found");
        }
        return _mapper.Map<Team>(_mapper.Map<ScorecardMgm.Common.Entities.Team>(team));

    }

    public Task UpdateTeamAsync(Team team)
    {
        var teamToUpdate = _teamRepository.GetTeam(team.TeamId);
        if (teamToUpdate == null)
        {
            throw new KeyNotFoundException("Team not found");
        }
        _teamRepository.UpdateTeam(_mapper.Map<ScorecardMgm.Common.Entities.Team>(team));
        return Task.CompletedTask;
    }
}
