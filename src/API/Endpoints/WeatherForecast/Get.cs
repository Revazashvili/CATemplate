using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Models;
using Application.Queries.WeatherForecasts;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.WeatherForecast
{
    [Route(WeatherForecastRoutes.Get)]
    public class Get : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IResponse<IReadOnlyList<GetWeatherForecastDto>>>
    {
        private readonly IMediator _mediator;
        public Get(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns all weather forecast",
            Summary = "Returns all weather forecast",
            OperationId = "WeatherForecast.Get",
            Tags = new []{ "WeatherForecast" })]
        [SwaggerResponse(StatusCodes.Status200OK,"All Weather Forecast Retrieved From Database.",typeof(IResponse<IReadOnlyList<GetWeatherForecastDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No Weather Forecast Were Found",typeof(IResponse<IReadOnlyList<GetWeatherForecastDto>>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<IReadOnlyList<GetWeatherForecastDto>>>> HandleAsync(
            CancellationToken cancellationToken = new())
        {
            return Ok(await _mediator.Send(new GetWeatherForecastsQuery(), cancellationToken));
        }
    }
}