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
using Forbids;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WeatherForecasts
{
    public record GetWeatherForecastQuery(Expression<Func<WeatherForecast,bool>> Predicate) : IRequestWrapper<GetWeatherForecastDto>{}
    
    public class GetWeatherForecastQueryHandler : IHandlerWrapper<GetWeatherForecastQuery,GetWeatherForecastDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IForbid _forbid;

        public GetWeatherForecastQueryHandler(IApplicationDbContext context, IMapper mapper,IForbid forbid)
        {
            _context = context;
            _mapper = mapper;
            _forbid = forbid;
        }

        public async Task<IResponse<GetWeatherForecastDto>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var weatherForecast = await _context.WeatherForecasts.FirstOrDefaultAsync(request.Predicate,cancellationToken);
            _forbid.Null(weatherForecast, new WeatherForecastNotFoundException());
            return Response.Success(_mapper.Map<GetWeatherForecastDto>(weatherForecast));
        }
    }
}