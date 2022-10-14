using ScorecardMgm.API.Interfaces;
using ScorecardMgm.API.Contracts;
using ScorecardMgm.Common.Filters;
using ScorecardMgm.API.APIroutes;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ScorecardMgm.API.Controllers;

[ApiController]

public class OverController : ControllerBase
{
    private readonly IOverService _overService;
    private readonly IMapper _mapper;

    public OverController(IOverService overService, IMapper mapper)
    {
        _overService = overService;
        _mapper = mapper;
    }

    [HttpGet(Routes.Over.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] OverFilter filter)
    {
        var overs = await _overService.GetAllOversAsync(filter);
        return Ok(overs);
    }

    [HttpGet(Routes.Over.Get)]
    public async Task<IActionResult> Get([FromRoute] string overId)
    {
        var over = await _overService.GetOverAsync(overId);
        return Ok(over);
    }

    [HttpPost(Routes.Over.Create)]
    public async Task<IActionResult> Create([FromBody] Over overRequest, [FromRoute] string matchId, [FromRoute] string tournamentId)
    {
        var over = _mapper.Map<Models.Over>(overRequest);
        over.OverId = Guid.NewGuid().ToString();
        await _overService.AddOverAsync(tournamentId, matchId, over);
        return Ok(over);
    }

    [HttpPut(Routes.Over.Update)]
    public async Task<IActionResult> Update([FromRoute] string overId, [FromBody] Over overRequest)
    {
        var over = _mapper.Map<Models.Over>(overRequest);
        over.OverId = overId;
        await _overService.UpdateOverAsync(overId, over);
        return Ok(over);
    }

    [HttpDelete(Routes.Over.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string overId)
    {
        await _overService.DeleteOverAsync(overId);
        return NoContent();
    }
}