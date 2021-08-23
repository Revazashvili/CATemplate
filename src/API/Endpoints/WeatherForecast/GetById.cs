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
    [Route(WeatherForecastRoutes.GetById)]
    public class GetById : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<IResponse<GetWeatherForecastDto>>
    {
        private readonly IMediator _mediator;

        public GetById(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        [SwaggerOperation(Description = "Returns weather forecast by id",
            Summary = "Returns single weather forecast",
            OperationId = "WeatherForecast.GetById",
            Tags = new []{ "WeatherForecast" })]
        [SwaggerResponse(StatusCodes.Status200OK,"Weather Forecast Retrieved From Database.",typeof(IResponse<GetWeatherForecastDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest,"No Weather Forecast Were Found",typeof(IResponse<GetWeatherForecastDto>))]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public override async Task<ActionResult<IResponse<GetWeatherForecastDto>>> HandleAsync([FromQuery,SwaggerParameter("Weather Forecast Id To Be Retrieved")]int id, CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(new GetWeatherForecastQuery(x => x.Id == id), cancellationToken);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}