using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.WeatherForecasts;
using Application.Common.Models;
using Application.Queries.WeatherForecasts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class WeatherForecastController : WrapperController
    {
        public WeatherForecastController(IServiceProvider provider) : base(provider) { }

        [HttpGet]
        public async Task<IResponse<IReadOnlyList<WeatherForecastDto>>> Get(CancellationToken cancellationToken) =>
            await Mediator.Send(new GetWeatherForecastQuery(),cancellationToken);

        [HttpPost]
        public async Task<IResponse<int>> Create([FromBody] CreateWeatherForecastCommand command,
            CancellationToken cancellationToken) => await Mediator.Send(command, cancellationToken);
        
    }
}