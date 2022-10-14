using ScorecardMgm.API.Models;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.API.Interfaces;
using ScorecardMgm.Common.Repositories.Interfaces;
using AutoMapper;

namespace ScorecardMgm.API.Implementations;

public class OverService : IOverService
{
    private readonly IOverRepository _overRepository;
    private readonly ITournamentRepository _tournamentRepository;
    private readonly IMatchRepository _matchRepository;
    private readonly IMapper _mapper;

    public OverService(IOverRepository overRepository, ITournamentRepository tournamentRepository, IMatchRepository matchRepository, IMapper mapper)
    {
        _overRepository = overRepository;
        _tournamentRepository = tournamentRepository;
        _matchRepository = matchRepository;
        _mapper = mapper;
    }

    public Task<Over> AddOverAsync(string tournamentId, string matchId, Over over)
    {
        var tournament = _tournamentRepository.GetTournament(tournamentId);
        if (tournament == null)
        {
            throw new Exception("Tournament not found");
        }

        var match = _matchRepository.GetMatch(matchId);
        if (match == null)
        {
            throw new Exception("Match not found");
        }

        _overRepository.AddOver(_mapper.Map<ScorecardMgm.Common.Entities.Over>(over));

        return Task.FromResult(over);
    }

    public Task<Over> DeleteOverAsync(string overId)
    {
        var over = _overRepository.GetOver(overId);
        if (over == null)
        {
            throw new Exception("Over not found");
        }
        _overRepository.DeleteOver(overId);
        return Task.FromResult(_mapper.Map<Over>(over));
    }

    public Task<List<Over>> GetAllOversAsync(OverFilter overFilter)
    {
        _overRepository.GetAllOversFromAMatch(_mapper.Map<ScorecardMgm.Common.Filters.OverFilter>(overFilter));
        return Task.FromResult(_mapper.Map<List<Over>>(_overRepository.GetAllOversFromAMatch(_mapper.Map<ScorecardMgm.Common.Filters.OverFilter>(overFilter))));
    }

    public Task<Over> GetOverAsync(string overId)
    {
        var over = _overRepository.GetOver(overId);
        if (over == null)
        {
            throw new Exception("Over not found");
        }
        return Task.FromResult(_mapper.Map<Over>(over));
    }

    public Task<Over> UpdateOverAsync(string id, Over over)
    {
        var getOver = _overRepository.GetOver(id);
        if (getOver == null)
        {
            throw new Exception("Over not found");
        }

        _overRepository.UpdateOver(_mapper.Map<ScorecardMgm.Common.Entities.Over>(over));

        return Task.FromResult(over);
    }
}