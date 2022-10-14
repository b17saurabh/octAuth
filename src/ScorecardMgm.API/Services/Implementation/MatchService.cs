using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;
using Microsoft.AspNetCore.Mvc;
using ScorecardMgm.API.Interfaces;
using ScorecardMgm.Common.Repositories.Interfaces;
using AutoMapper;

namespace ScorecardMgm.API.Implementations;

public class MatchService : IMatchService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IMapper _mapper;
    private readonly ITournamentRepository _tournamentRepository;

    public MatchService(IMatchRepository matchRepository, IMapper mapper, ITournamentRepository tournamentRepository)
    {
        _matchRepository = matchRepository;
        _mapper = mapper;
        _tournamentRepository = tournamentRepository;
    }



    public async Task AddMatchAsync(string tournamentId, Match match)
    {
        var tournament = _tournamentRepository.GetTournament(tournamentId);
        if (tournament == null)
        {
            throw new Exception("Tournament not found");
        }
        await _matchRepository.AddMatch(_mapper.Map<ScorecardMgm.Common.Entities.Match>(match));
    }

    public async Task DeleteMatchAsync(string matchId)
    {
        var match = _matchRepository.GetMatch(matchId);
        if (match == null)
        {
            throw new Exception("Match not found");
        }
        await _matchRepository.DeleteMatch(matchId);
    }

    public Task<List<Match>> GetAllMatchesAsync(MatchFilter matchFilter)
    {
        return Task.FromResult(_mapper.Map<List<Match>>(_matchRepository.GetAllMatches(_mapper.Map<ScorecardMgm.Common.Filters.MatchFilter>(matchFilter))));
    }

    public Task<Match> GetMatchAsync(string matchId)
    {
        var match = _matchRepository.GetMatch(matchId);
        if (match == null)
        {
            throw new Exception("Match not found");
        }
        return Task.FromResult(_mapper.Map<Match>(match));
    }

    public async Task UpdateMatchAsync(Match match)
    {
        var matchToUpdate = _matchRepository.GetMatch(match.MatchId);
        if (matchToUpdate == null)
        {
            throw new Exception("Match not found");
        }
        await _matchRepository.UpdateMatch(_mapper.Map<ScorecardMgm.Common.Entities.Match>(match));
    }
}