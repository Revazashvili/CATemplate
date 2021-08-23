using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WeatherForecasts
{
    public record GetWeatherForecastQuery(Expression<Func<WeatherForecast,bool>> Predicate) : IRequestWrapper<GetWeatherForecastDto>{}
    
    public class GetWeatherForecastQueryHandler : IHandlerWrapper<GetWeatherForecastQuery,GetWeatherForecastDto>
    {
        private readonly IApplicationDbContext _context;

        public GetWeatherForecastQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResponse<GetWeatherForecastDto>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var weatherForecast =
                await _context.WeatherForecasts
                    .FirstOrDefaultAsync(request.Predicate, cancellationToken);
            return weatherForecast is not null
                ? Response.Success(new GetWeatherForecastDto(weatherForecast!.Id, weatherForecast.Date,
                    weatherForecast.TemperatureC, weatherForecast.Summary))
                : Response.Fail<GetWeatherForecastDto>("Can't find any weather forecast");
        }
    }
}