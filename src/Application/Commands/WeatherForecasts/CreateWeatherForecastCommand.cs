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

namespace Application.Commands.WeatherForecasts
{
    public record CreateWeatherForecastCommand(CreateWeatherForecastDto CreateWeatherForecastDto) : IRequestWrapper<long>{}
    
    public class CreateWeatherForecastCommandHandler : IHandlerWrapper<CreateWeatherForecastCommand,long>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IForbid _forbid;

        public CreateWeatherForecastCommandHandler(IApplicationDbContext context, IMapper mapper,IForbid forbid)
        {
            _context = context;
            _mapper = mapper;
            _forbid = forbid;
        }

        public async Task<IResponse<long>> Handle(CreateWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast = _mapper.Map<WeatherForecast>(request.CreateWeatherForecastDto);
            await _context.WeatherForecasts.AddAsync(weatherForecast, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            _forbid.LessThan(weatherForecast.Id, 1, new CreateWeatherForecastException());
            return Response.Success(weatherForecast.Id);
        }
    }
    
}