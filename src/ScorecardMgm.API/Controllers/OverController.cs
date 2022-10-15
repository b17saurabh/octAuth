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
        try
        {
            var overs = await _overService.GetAllOversAsync(filter);
            return Ok(overs);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet(Routes.Over.Get)]
    public async Task<IActionResult> Get([FromRoute] string overId)
    {
        try
        {
            var over = await _overService.GetOverAsync(overId);
            return Ok(over);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost(Routes.Over.Create)]
    public async Task<IActionResult> Create([FromBody] Over overRequest, [FromRoute] string matchId)
    {
        try
        {
            // var over = _mapper.Map<Models.Over>(overRequest);
            var over = await _overService.AddOverAsync(matchId, _mapper.Map<Models.Over>(overRequest));
            // over.OverId = Guid.NewGuid().ToString();
            return Ok(over);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut(Routes.Over.Update)]
    public async Task<IActionResult> Update([FromRoute] string overId, [FromBody] Over overRequest)
    {
        try
        {
            var overToBeUpdated = _mapper.Map<Models.Over>(overRequest);
            overToBeUpdated.OverId = overId;
            var over = await _overService.UpdateOverAsync(overId, overToBeUpdated);
            return Ok(over);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete(Routes.Over.Delete)]
    public async Task<IActionResult> Delete([FromRoute] string overId)
    {
        try
        {
            await _overService.DeleteOverAsync(overId);
            return Ok(overId + " deleted");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}