using MeterReading.DataAccess;
using MeterReading.Logic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace MeterReading.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Seed the test accounts
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<RepositoryContext>();
                string filepath = @$"{Directory.GetCurrentDirectory()}\ResourceFiles\Test_Accounts.csv";
                AccountSeeder.Seed(context, filepath);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging((context, logging) =>
             {
                 logging.ClearProviders();
                 logging.AddDebug()
                        .AddEventLog();
             })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
