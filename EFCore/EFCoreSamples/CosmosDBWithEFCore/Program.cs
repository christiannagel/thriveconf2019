using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CosmosDBWithEFCore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                using var host = Host
                    .CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration(configuration =>
                    {
#if DEBUG
                        configuration.AddUserSecrets("1E2D66CC-11C9-4DE7-B25E-F1EAA5F0154A");
#endif
                    })
                    .ConfigureServices((context, services) =>
                    {
                        IConfigurationSection configSection = context.Configuration.GetSection("CosmosSettings");

                        services.AddDbContext<BooksContext>(
                            options => options.UseCosmos(
                            configSection["ServiceEndpoint"],
                            configSection["AuthKey"],
                            configSection["DatabaseName"]));

                        services.AddTransient<BooksService>();

                        services.AddLogging(options =>
                            options.AddDebug().SetMinimumLevel(LogLevel.Trace));

                    }).Build();

                var service = host.Services.GetRequiredService<BooksService>();
                await service.CreateTheDatabaseAsync();
                await service.WriteBooksAsync();
                service.ReadBooks();
                Console.WriteLine("completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
