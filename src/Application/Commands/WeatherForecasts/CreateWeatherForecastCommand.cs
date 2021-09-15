using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Entities;
using Domain.Exceptions.WeatherForecast;
using MapsterMapper;

namespace Application.Commands.WeatherForecasts
{
    public record CreateWeatherForecastCommand(CreateWeatherForecastDto CreateWeatherForecastDto) : IRequestWrapper<long>{}
    
    public class CreateWeatherForecastCommandHandler : IHandlerWrapper<CreateWeatherForecastCommand,long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateWeatherForecastCommandHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);

        public async Task<IResponse<long>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast = _mapper.Map<WeatherForecast>(request.CreateWeatherForecastDto);
            await _context.WeatherForecasts.AddAsync(weatherForecast, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            if (weatherForecast.Id > 0)
                throw new CreateWeatherForecastException();
            return Response.Success(weatherForecast.Id);
        }
    }
    
}