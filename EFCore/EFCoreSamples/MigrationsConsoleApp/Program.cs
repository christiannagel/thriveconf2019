using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MigrationsLib;

namespace MigrationsConsoleApp
{
    class Program
    {
        private const string ConnectionString = @"server=(localdb)\mssqllocaldb;database=ProCSharpMigrations;trusted_connection=true";

        static void Main()
        {
            using var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<MenusContext>(options =>
                        options.UseSqlServer(ConnectionString));
                }).Build();

            var context = host.Services.GetService<MenusContext>();
            context.Database.Migrate();
        }
    }
}
