using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class WrapperController : Controller
    {
        private readonly IMediator _mediator;
        public WrapperController(IServiceProvider provider)
        {
            _mediator = provider.GetService<IMediator>();
        }
        
        protected IMediator Mediator => _mediator ?? throw new Exception("Can't get IMediator interface");
    }
}