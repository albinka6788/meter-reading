using MeterReading.DataAccess;
using MeterReading.DataAccess.Contracts;
using MeterReading.DataAccess.Implementations;
using MeterReading.Logic.Contracts;
using MeterReading.Logic.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MeterReading.API.ServiceExtensions
{
    public static class ServiceExtension
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["sqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<IMeterReadingManager>>();
            services.AddSingleton(typeof(ILogger), logger);

            services.AddSingleton(configuration);
            services.AddScoped<ICsvHelper, Logic.Helper.CsvHelper>();
            services.AddScoped<IMeterReadingManager, MeterReadingManager>();
        }
    }
}
