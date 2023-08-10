using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CleanCodeArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private IMediator _mediatorInstance;
        private ICurrentUserService currentUserInstance;
        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ICurrentUserService CurrentUser =>
            currentUserInstance ??= HttpContext.RequestServices.GetService<ICurrentUserService>();
    }
}
