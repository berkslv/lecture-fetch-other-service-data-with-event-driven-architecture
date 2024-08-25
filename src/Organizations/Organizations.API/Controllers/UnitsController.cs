using Microsoft.AspNetCore.Mvc;
using Organizations.API.Controllers.Base;
using Organizations.Application.Features.Units.Commands.CreateUnit;
using Organizations.Application.Features.Units.Commands.CreateUnitAsync;
using Organizations.Application.Features.Units.Queries.GetUnits;

namespace Organizations.API.Controllers;

[Route("api/units")]
public class UnitsController : BaseController
{

    [HttpPost("sync")]
    public async Task<ActionResult<Guid>> CreateUnit([FromQuery] CreateUnitCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPost("async")]
    public async Task<ActionResult<Guid>> CreateUnit([FromQuery] CreateUnitAsyncCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GetUnitsQueryResponse>>> GetUnits()
    {
        return await Mediator.Send(new GetUnitsQuery());
    }
}
