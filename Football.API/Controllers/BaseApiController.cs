using Football.API.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Football.API.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??=
                      HttpContext.RequestServices.GetService<IMediator>();

        private  IUpdateDatabaseService _updateDatabaseService;
        protected IUpdateDatabaseService UpdateService => _updateDatabaseService ??=
              HttpContext.RequestServices.GetService<IUpdateDatabaseService>();
    }
}
