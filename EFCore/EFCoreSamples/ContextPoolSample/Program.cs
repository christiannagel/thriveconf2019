using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ContextPoolSample
{
    class Program
    {
        static async Task Main()
        {
            using var host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {

                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("BooksConnection");
                    services.AddTransient<BooksController>();
                    services.AddTransient<BooksService>();
                    services.AddDbContextPool<BooksContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });
                    services.AddLogging();
                }).Build();

            var p = new Program();
            await host.Services.GetService<BooksController>().CreateDatabaseAsync();
            await host.Services.GetService<BooksController>().AddBooksAsync();
            await host.Services.GetService<BooksController>().ReadBooksAsync();
            await host.Services.GetService<BooksController>().ReadBooksAsync();
            await host.Services.GetService<BooksController>().ReadBooksAsync();
        }
    }
}
