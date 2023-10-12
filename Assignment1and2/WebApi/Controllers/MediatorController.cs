using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	public class MediatorController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}

