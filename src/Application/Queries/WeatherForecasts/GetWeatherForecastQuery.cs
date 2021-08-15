using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WeatherForecasts
{
    public record GetWeatherForecastQuery(int id) : IRequestWrapper<IReadOnlyList<WeatherForecastDto>>{}
    
    public class GetWeatherForecastQueryHandler : IHandlerWrapper<GetWeatherForecastQuery,IReadOnlyList<WeatherForecastDto>>
    {
        private readonly IApplicationDbContext _context;
        public GetWeatherForecastQueryHandler(IApplicationDbContext context) { _context = context; }

        public async Task<IResponse<IReadOnlyList<WeatherForecastDto>>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<WeatherForecastDto> weatherForecasts = await _context.WeatherForecasts
                .Select(w => new WeatherForecastDto(w.Id, w.Date, w.TemperatureC, w.Summary))
                .ToListAsync(cancellationToken);
            
            return weatherForecasts.Any()
                ? Response.Success(weatherForecasts)
                : Response.Fail<IReadOnlyList<WeatherForecastDto>>("Can't find any record");
        }
    }
    
    public record WeatherForecastDto(long Id, DateTime Date, int TemperatureC, string Summary)
    {
        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
    }
}