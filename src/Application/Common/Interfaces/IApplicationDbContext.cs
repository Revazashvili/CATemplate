using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    /// <summary>
    /// Abstraction for <see cref="DbContext"/> class.
    /// </summary>
    public interface IApplicationDbContext : IDisposable
    {
        DbSet<WeatherForecast> WeatherForecasts { get; set; }
        
        /// <summary>
        /// Saves changes to database.
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> instance.</param>
        /// <returns>Affected row count.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}