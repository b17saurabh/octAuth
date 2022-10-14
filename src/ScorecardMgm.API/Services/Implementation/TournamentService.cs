
using AutoMapper;
using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.Common.Repositories.Interfaces;

namespace ScorecardMgm.API.Implementations;

public class TournamentService : ITournamentService
{
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMapper _mapper;

    public TournamentService(ITournamentRepository tournamentRepository, IMapper mapper)
    {
        _tournamentRepository = tournamentRepository;
        _mapper = mapper;
    }

    public async Task AddTournamentAsync(Tournament tournament)
    {
        await _tournamentRepository.AddTournament(_mapper.Map<ScorecardMgm.Common.Entities.Tournament>(tournament));
    }

    public async Task DeleteTournamentAsync(string tournamentId)
    {
        var tournament = _tournamentRepository.GetTournament(tournamentId);
        if (tournament == null)
        {
            throw new KeyNotFoundException("Tournament not found");
        }
        await _tournamentRepository.DeleteTournament(tournamentId);
    }

    public async Task<List<Tournament>> GetAllTournamentsAsync(TournamentFilter tournamentFilter)
    {
        return _mapper.Map<List<Tournament>>(await _tournamentRepository.GetAllTournaments(_mapper.Map<ScorecardMgm.Common.Filters.TournamentFilter>(tournamentFilter)));
    }

    public async Task<Tournament> GetTournamentAsync(string tournamentId)
    {
        var tournament = _tournamentRepository.GetTournament(tournamentId);
        if (tournament == null)
        {
            throw new KeyNotFoundException("Tournament not found");
        }
        var tt = await Task.FromResult(_mapper.Map<Tournament>(tournament));
        return tt;
    }

    public async Task UpdateTournamentAsync(Tournament tournament)
    {
        var tournamentToUpdate = await _tournamentRepository.GetTournament(tournament.TournamentId);
        if (tournamentToUpdate == null)
        {
            throw new KeyNotFoundException("Tournament not found");
        }
        await _tournamentRepository.UpdateTournament(_mapper.Map<ScorecardMgm.Common.Entities.Tournament>(tournament));

    }
}

