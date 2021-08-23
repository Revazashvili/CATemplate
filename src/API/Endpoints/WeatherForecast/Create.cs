using System;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.WeatherForecasts;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.WeatherForecast
{
    [Route(WeatherForecastRoutes.Create)]
    public class Create : BaseAsyncEndpoint
        .WithRequest<CreateWeatherForecastDto>
        .WithResponse<IResponse<int>>
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator) => _mediator = mediator;
        
        [HttpPost]
        [SwaggerOperation(Description = "Creates new WeatherForecast and returns affected row number.",
            Summary = "Creates new WeatherForecast.",
            OperationId = "WeatherForecast.Create",
            Tags = new []{ "WeatherForecast" })]
        [SwaggerResponse(StatusCodes.Status201Created,"New Weather Forecast Created Successfully.",typeof(IResponse<int>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error Occurred While Creating Weather Forecast.",typeof(IResponse<int>))]
        [Produces("application/json")]
        [Consumes("application/json")]
        public override async Task<ActionResult<IResponse<int>>> HandleAsync(
            [FromBody,SwaggerRequestBody(Required = true,Description = "Create Weather Forecast Payload")] CreateWeatherForecastDto request,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new CreateWeatherForecastCommand(request), cancellationToken);
            return result.Succeeded ? Created(new Uri($"https://localhost:5001/api/WeatherForecast/{result.Data}"), result) : BadRequest(result);
        }
    }
}