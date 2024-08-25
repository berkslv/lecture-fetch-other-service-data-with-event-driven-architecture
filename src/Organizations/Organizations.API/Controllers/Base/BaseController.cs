using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Organizations.API.Controllers.Base;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator
    {
        get
        {
            return _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        }
    }
}