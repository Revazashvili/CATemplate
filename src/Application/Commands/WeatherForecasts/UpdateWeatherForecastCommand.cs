using System.Threading;
using System.Threading.Tasks;
using Application.Common.DTOs.WeatherForecast;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.WeatherForecasts
{
    public record UpdateWeatherForecastCommand(UpdateWeatherForecastDto UpdateWeatherForecastDto) : IRequestWrapper<int>{}
    
    public class UpdateWeatherForecastCommandHandler : IHandlerWrapper<UpdateWeatherForecastCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateWeatherForecastCommandHandler(IApplicationDbContext context, IMapper mapper) =>
            (_context, _mapper) = (context, mapper);
        
        public async Task<IResponse<int>> Handle(UpdateWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast =
                await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == request.UpdateWeatherForecastDto.Id, cancellationToken);
            if (weatherForecast is null) return Response.Fail<int>("Can't be found weather forecast with this id");
            _mapper.Map(request.UpdateWeatherForecastDto, weatherForecast);
            var updateRowCount = await _context.SaveChangesAsync(cancellationToken);
            return updateRowCount > 0
                ? Response.Success(updateRowCount)
                : Response.Fail<int>("Can't update weather forecast");
        }
    }
}