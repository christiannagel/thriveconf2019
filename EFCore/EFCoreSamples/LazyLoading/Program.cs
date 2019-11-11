using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LazyLoading
{
    class Program
    {
        private const string BooksConnection = nameof(BooksConnection);

        static async Task Main()
        {
            using var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(config =>
                     {
                         config
                             .AddConsole()
                             .AddDebug()
                             .AddFilter(level => level > LogLevel.Debug);
                     })
                    .AddTransient<BooksService>()
                    .AddDbContext<BooksContext>(options =>
                    {
                        options
                            .UseLazyLoadingProxies()
                            .UseSqlServer(context.Configuration.GetConnectionString(BooksConnection));
                    });
                }).Build();

            var booksService = host.Services.GetRequiredService<BooksService>();
            await booksService.CreateDatabaseAsync();
            booksService.GetBooksWithLazyLoading();
            booksService.GetBooksWithEagerLoading();
            booksService.GetBooksWithExplicitLoading();
            await booksService.DeleteDatabaseAsync();
        }
    }
}
