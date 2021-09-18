using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Wrappers;
using Domain.Exceptions.WeatherForecast;
using Forbids;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.WeatherForecasts
{
    public record DeleteWeatherForecastCommand(long Id) : IRequestWrapper<int>{}
    
    public class DeleteWeatherForecastCommandHandler : IHandlerWrapper<DeleteWeatherForecastCommand,int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IForbid _forbid;

        public DeleteWeatherForecastCommandHandler(IApplicationDbContext context,IForbid forbid)
        {
            _context = context;
            _forbid = forbid;
        }

        public async Task<IResponse<int>> Handle(DeleteWeatherForecastCommand request, CancellationToken cancellationToken)
        {
            var weatherForecast = await _context.WeatherForecasts.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
            _forbid.Null(weatherForecast,new WeatherForecastNotFoundException());
            _context.WeatherForecasts.Remove(weatherForecast);
            var deletedRowCount = await _context.SaveChangesAsync(cancellationToken);
            _forbid.LessThan(deletedRowCount, 1, new DeleteWeatherForecastException());
            return Response.Success(deletedRowCount);
        }
    }
}