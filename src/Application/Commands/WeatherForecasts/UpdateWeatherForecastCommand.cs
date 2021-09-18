using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Exceptions.WeatherForecast;
using Forbids;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.WeatherForecasts
{
    public record UpdateWeatherForecastCommand(UpdateWeatherForecastDto UpdateWeatherForecastDto) : IRequestWrapper<int>{}
    
    public class UpdateWeatherForecastCommandHandler : IHandlerWrapper<UpdateWeatherForecastCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IForbid _forbid;

        public UpdateWeatherForecastCommandHandler(IApplicationDbContext context, IMapper mapper,IForbid forbid)
        {
            _context = context;
            _mapper = mapper;
            _forbid = forbid;
        }
        
        public async Task<IResponse<int>> Handle(UpdateWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast =
                await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == request.UpdateWeatherForecastDto.Id, cancellationToken);
            _forbid.Null(weatherForecast, new WeatherForecastNotFoundException());
            _mapper.Map(request.UpdateWeatherForecastDto, weatherForecast);
            var updateRowCount = await _context.SaveChangesAsync(cancellationToken);
            _forbid.LessThan(updateRowCount, 1, new UpdateWeatherForecastException());
            return Response.Success(updateRowCount);
        }
    }
}