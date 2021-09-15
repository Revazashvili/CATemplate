using System;
using System.Net.Mime;
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
        .WithResponse<IResponse<long>>
    {
        private readonly IMediator _mediator;

        public Create(IMediator mediator) => _mediator = mediator;
        
        [HttpPost]
        [SwaggerOperation(Description = "Creates new WeatherForecast and returns created entity id and location where resource is created.",
            Summary = "Creates new WeatherForecast.",
            OperationId = "WeatherForecast.Create",
            Tags = new []{ "WeatherForecast" })]
        [SwaggerResponse(StatusCodes.Status201Created,"New Weather Forecast Created Successfully.",typeof(IResponse<long>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error Occurred While Creating Weather Forecast.",typeof(IResponse<long>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<long>>> HandleAsync(
            [FromBody,SwaggerRequestBody(Required = true,Description = "Create Weather Forecast Payload")] CreateWeatherForecastDto request,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new CreateWeatherForecastCommand(request), cancellationToken);
            return Created(
                new Uri(
                    $"{Request.Scheme}://{Request.Host.ToString()}/{WeatherForecastRoutes.GetById}?id={result.Data}"),
                result);
        }
    }
}