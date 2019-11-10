using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LazyLoading
{
    class Program
    {
        static async Task Main()
        {
            var container = AppServices.Instance.Container;
            var booksService = container.GetRequiredService<BooksService>();
            await booksService.CreateDatabaseAsync();
            booksService.GetBooksWithLazyLoading();
            booksService.GetBooksWithEagerLoading();
            booksService.GetBooksWithExplicitLoading();
            await booksService.DeleteDatabaseAsync();
        }
    }
}
