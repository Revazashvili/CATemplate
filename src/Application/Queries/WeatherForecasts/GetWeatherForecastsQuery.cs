using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Exceptions.WeatherForecast;
using Forbids;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.WeatherForecasts
{
    public record GetWeatherForecastsQuery : IRequestWrapper<IReadOnlyList<GetWeatherForecastDto>>{}
    
    public class GetWeatherForecastsQueryHandler : IHandlerWrapper<GetWeatherForecastsQuery,IReadOnlyList<GetWeatherForecastDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IForbid _forbid;

        public GetWeatherForecastsQueryHandler(IApplicationDbContext context, IMapper mapper,IForbid forbid)
        {
            _context = context;
            _mapper = mapper;
            _forbid = forbid;
        }

        public async Task<IResponse<IReadOnlyList<GetWeatherForecastDto>>> Handle(GetWeatherForecastsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<GetWeatherForecastDto> weatherForecasts = await _context.WeatherForecasts
                .ProjectToType<GetWeatherForecastDto>()
                .ToListAsync(cancellationToken);
            _forbid.False(weatherForecasts.Any(), new WeatherForecastNotFoundException());
            return Response.Success(weatherForecasts);
        }
    }
}