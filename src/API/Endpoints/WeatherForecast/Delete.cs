using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using API.Routes;
using Application.Commands.WeatherForecasts;
using Application.Common.Models;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.WeatherForecast
{
    [Route(WeatherForecastRoutes.Delete)]
    public class Delete : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<IResponse<int>>
    {
        private readonly IMediator _mediator;

        public Delete(IMediator mediator) => _mediator = mediator;
        
        [HttpDelete]
        [SwaggerOperation(Description = "Deletes weather forecast and returns affected row number.",
            Summary = "Deletes Weather Forecast",
            OperationId = "WeatherForecast.Delete",
            Tags = new []{ "WeatherForecast" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Weather Forecast Deleted Successfully.",typeof(IResponse<int>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"Error Occurred While Deleting Weather Forecast.",typeof(IResponse<int>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<int>>> HandleAsync(
            [FromQuery,SwaggerParameter("Weather Forecast Id",Required = true)]int id,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new DeleteWeatherForecastCommand(id), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}