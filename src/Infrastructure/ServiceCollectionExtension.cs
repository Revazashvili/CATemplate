using System;
using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// Extension class for <see cref="IServiceCollection"/> interface
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Injects infrastructure dependencies into dependency injection container
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface</param>
        /// <param name="configuration"><see cref="IConfiguration"/> interface</param>
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration.GetSection("UseInMemoryDatabase").Value))
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });
            }
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);
        }
    }
}