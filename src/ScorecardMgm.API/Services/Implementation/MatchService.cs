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
        var tournament = await _tournamentRepository.GetTournament(tournamentId);
        if (tournament == null)
        {
            throw new Exception("Tournament not found");
        }
        await _matchRepository.AddMatch(_mapper.Map<ScorecardMgm.Common.Entities.Match>(match));
    }

    public async Task DeleteMatchAsync(string matchId)
    {
        // var match = _matchRepository.GetMatch(matchId);
        // if (match == null)
        // {
        //     throw new Exception("Match not found");
        // }
        await _matchRepository.DeleteMatch(matchId);
    }

    public async Task<List<Match>> GetAllMatchesAsync(MatchFilter matchFilter)
    {
        return _mapper.Map<List<Match>>(await _matchRepository.GetAllMatches(_mapper.Map<ScorecardMgm.Common.Filters.MatchFilter>(matchFilter)));
    }

    public async Task<Match> GetMatchAsync(string matchId)
    {
        var match = await _matchRepository.GetMatch(matchId);
        // if (match == null)
        // {
        //     throw new Exception("Match not found");
        // }
        return _mapper.Map<Match>(_mapper.Map<ScorecardMgm.Common.Entities.Match>(match));
    }

    public async Task UpdateMatchAsync(Match match)
    {
        // var matchFromDB = _matchRepository.GetMatch(match.MatchId);
        // if (matchFromDB == null)
        // {
        //     throw new Exception("Match not found");
        // }
        // match.TournamentId = matchFromDB.TournamentId;
        await _matchRepository.UpdateMatch(_mapper.Map<ScorecardMgm.Common.Entities.Match>(match));
    }
}