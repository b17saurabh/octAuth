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

    public async Task<Over> AddOverAsync(string matchId, Over over)
    {
        // var tournament = await _tournamentRepository.GetTournament(tournamentId);
        // if (tournament == null)
        // {
        //     throw new Exception("Tournament not found");
        // }

        over.MatchId = matchId;
        var match = await _matchRepository.GetMatch(over.MatchId);
        if (match == null)
        {
            throw new Exception("Match not found");
        }
        over.OverId = Guid.NewGuid().ToString();
        await _overRepository.AddOver(_mapper.Map<ScorecardMgm.Common.Entities.Over>(over));

        return over;
    }

    public async Task DeleteOverAsync(string overId)
    {

        await _overRepository.DeleteOver(overId);
        // return _mapper.Map<Over>(_mapper.Map<ScorecardMgm.Common.Entities.Over>(over));
    }

    public async Task<List<Over>> GetAllOversAsync(OverFilter overFilter)
    {
        // _overRepository.GetAllOversFromAMatch(_mapper.Map<ScorecardMgm.Common.Filters.OverFilter>(overFilter));
        return _mapper.Map<List<Over>>(await _overRepository.GetAllOvers(_mapper.Map<ScorecardMgm.Common.Filters.OverFilter>(overFilter)));
    }

    public async Task<Over> GetOverAsync(string overId)
    {
        var over = await _overRepository.GetOver(overId);
        if (over == null)
        {
            throw new Exception("Over not found");
        }
        return _mapper.Map<Over>(_mapper.Map<ScorecardMgm.Common.Entities.Over>(over));
    }

    public async Task<Over> UpdateOverAsync(string id, Over over)
    {
        var getOver = _overRepository.GetOver(id);
        over.MatchId = getOver.Result.MatchId;
        // if (getOver == null)
        // {
        //     throw new Exception("Over not found");
        // }

        await _overRepository.UpdateOver(_mapper.Map<ScorecardMgm.Common.Entities.Over>(over));

        return over;
        // return _mapper.Map<Over>(updatedOver);
    }
}