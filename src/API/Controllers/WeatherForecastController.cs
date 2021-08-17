using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Commands.WeatherForecasts;
using Application.Common.DTOs.WeatherForecast;
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
            await Mediator.Send(new GetWeatherForecastsQuery(),cancellationToken);
        
        [HttpGet]
        public async Task<IResponse<WeatherForecastDto>> GetById(long Id,CancellationToken cancellationToken) =>
            await Mediator.Send(new GetWeatherForecastQuery(x=>x.Id == Id),cancellationToken);

        [HttpPost]
        public async Task<IResponse<int>> Create([FromBody] CreateWeatherForecastCommand createCommand,
            CancellationToken cancellationToken) => await Mediator.Send(createCommand, cancellationToken);

        [HttpPut]
        public async Task<IResponse<int>> Update([FromBody] UpdateWeatherForecastCommand updateCommand,
            CancellationToken cancellationToken) => await Mediator.Send(updateCommand, cancellationToken);
        
        [HttpDelete]
        public async Task<IResponse<int>> Delete([FromBody] DeleteWeatherForecastCommand deleteCommand,
            CancellationToken cancellationToken) => await Mediator.Send(deleteCommand, cancellationToken);

    }
}