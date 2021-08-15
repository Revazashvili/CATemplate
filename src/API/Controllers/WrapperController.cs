using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    /// <summary>
    /// Wrapper Controller with Mediator Instance
    /// </summary>
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
        /// <summary>
        /// Mediator instance that should be used in derived controllers to send requests to specific handlers
        /// </summary>
        protected IMediator Mediator => _mediator ?? throw new Exception("Can't get IMediator interface");
    }
}