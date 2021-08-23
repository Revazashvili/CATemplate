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
    [Route(WeatherForecastRoutes.Update)]
    public class Update : BaseAsyncEndpoint
        .WithRequest<UpdateWeatherForecastDto>
        .WithResponse<IResponse<int>>
    {
        private readonly IMediator _mediator;

        public Update(IMediator mediator) => _mediator = mediator;
        
        [HttpPut]
        [SwaggerOperation(Description = "Updates weather forecast and returns affected row number.",
            Summary = "Updates weather forecast",
            OperationId = "WeatherForecast.Update",
            Tags = new []{ "WeatherForecast" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Weather Forecast Updated Successfully.",typeof(IResponse<int>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error Occurred While Updating Weather Forecast.",typeof(IResponse<int>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<int>>> HandleAsync(
            [FromBody,SwaggerRequestBody("Update Weather Forecast Payload")]UpdateWeatherForecastDto request, 
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new UpdateWeatherForecastCommand(request), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}