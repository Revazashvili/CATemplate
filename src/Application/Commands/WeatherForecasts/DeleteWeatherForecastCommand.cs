using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.WeatherForecasts
{
    public record DeleteWeatherForecastCommand(long Id) : IRequestWrapper<int>{}
    
    public class DeleteWeatherForecastCommandHandler : IHandlerWrapper<DeleteWeatherForecastCommand,int>
    {
        private readonly IApplicationDbContext _context;

        public DeleteWeatherForecastCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResponse<int>> Handle(DeleteWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
            if (weatherForecast is null)
                throw new Exception("Can't find weather forecast with this id");
            _context.WeatherForecasts.Remove(weatherForecast);
            var deletedRowCount = await _context.SaveChangesAsync(cancellationToken);
            if (deletedRowCount > 0)
                throw new Exception("Error Occurred While Deleting Weather Forecast");
            return Response.Success<int>(deletedRowCount);
        }
    }
}