using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    /// <summary>
    /// Extension class for <see cref="IServiceCollection"/> interface
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds cross-origin resource sharing services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> interface</param>
        /// <param name="configuration"><see cref="IConfiguration"/> interface</param>
        /// <param name="policyName">Policy Name</param>
        public static void ConfigureCors(this IServiceCollection services,IConfiguration configuration,string policyName)
        {
            var cors = configuration.GetValue<string[]>("ApiCorsPolicy");
            services.AddCors(options => options.AddPolicy(policyName, builder =>
            {
                builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins(cors)
                    .AllowCredentials();
            }));
        }
    }
}