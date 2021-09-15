using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using Domain.Exceptions.WeatherForecast;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WeatherForecasts
{
    public record GetWeatherForecastQuery(Expression<Func<WeatherForecast,bool>> Predicate) : IRequestWrapper<GetWeatherForecastDto>{}
    
    public class GetWeatherForecastQueryHandler : IHandlerWrapper<GetWeatherForecastQuery,GetWeatherForecastDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetWeatherForecastQueryHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IResponse<GetWeatherForecastDto>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var weatherForecast = await _context.WeatherForecasts.FirstOrDefaultAsync(request.Predicate,cancellationToken);
            if (weatherForecast is null)
                throw new WeatherForecastNotFoundException();
            return Response.Success(_mapper.Map<GetWeatherForecastDto>(weatherForecast));
        }
    }
}