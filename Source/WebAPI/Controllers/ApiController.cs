using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MakeMeRich.WebAPI.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
