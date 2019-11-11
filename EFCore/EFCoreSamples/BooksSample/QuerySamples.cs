using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksSample
{
    public class QuerySamples
    {
        public static async Task QueryAllBooksAsync()
        {
            Console.WriteLine(nameof(QueryAllBooksAsync));
            using var context = new BooksContext();
            List<Book> books = await context.Books.TagWith("QueryAllBooks").ToListAsync();
            foreach (var b in books)
            {
                Console.WriteLine(b);
            }
            Console.WriteLine();
        }

        public static async Task QueryAllBooksWithAsyncEnumerableAsync()
        {
            Console.WriteLine(nameof(QueryAllBooksWithAsyncEnumerableAsync));
            using var context = new BooksContext();
            await foreach (var b in context.Books.AsAsyncEnumerable())
            {
                Console.WriteLine(b);
            };
            Console.WriteLine();
        }

        public static async Task QueryBookByKeyAsync(int id)
        {
            Console.WriteLine(nameof(QueryBookByKeyAsync));
            using var context = new BooksContext();
            Book b = await context.Books.FindAsync(id);
            if (b != null)
            {
                Console.WriteLine($"found book {b}");
            }
            Console.WriteLine();
        }

        public static async Task QueryBooksAsync()
        {
            using var context = new BooksContext();
            List<Book> wroxBooks = await context.Books
                .Where(b => b.Publisher == "Wrox Press")
                .TagWith("QueryBooks")
                .ToListAsync();

            foreach (var b in wroxBooks)
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }
            Console.WriteLine();
        }

        public static async Task QueryBookAsync(string title)
        {
            Console.WriteLine(nameof(QueryBookAsync));
            try
            {
                using var context = new BooksContext();
                Book book = await context.Books.TagWith("QueryBook").FirstOrDefaultAsync(b => b.Title == title);
                if (book != null)
                {
                    Console.WriteLine($"found book {book}");
                }
            }
            catch (InvalidOperationException ex) when (ex.HResult == -2146233079) // more than 1 element
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
        }

        public static async Task FilterBooksAsync(string title)
        {
            Console.WriteLine(nameof(FilterBooksAsync));
            using var context = new BooksContext();

            List<Book> wroxBooks = await context.Books
                .Where(b => b.Title.Contains(title))
                .TagWith("FilterBooks")
                .ToListAsync();

            foreach (var b in wroxBooks)
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }

            Console.WriteLine();
        }

        public static async Task RawSqlQuery(string publisher)
        {
            Console.WriteLine(nameof(RawSqlQuery));
            using var context = new BooksContext();
            IList<Book> books = await context.Books.FromSqlInterpolated($"SELECT * FROM Books WHERE Publisher = {publisher}").ToListAsync();

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }
            Console.WriteLine();
        }

        public static async Task UseEFFunctions(string titleSegment)
        {
            Console.WriteLine(nameof(UseEFFunctions));
            using var context = new BooksContext();
            string likeExpression = $"%{titleSegment}%";

            IList<Book> books = await context.Books.TagWith("UseEFFunctions").Where(b => EF.Functions.Like(b.Title, likeExpression)).ToListAsync();
            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }
            Console.WriteLine();
        }

        public static void CompileQuery()
        {
            Console.WriteLine(nameof(CompileQuery));
            Func<BooksContext, string, IEnumerable<Book>> compiledQuery = 
                EF.CompileQuery<BooksContext, string, Book>(
                    (context, publisher) 
                        => context.Books
                            .TagWith("Compiled")
                            .Where(b => b.Publisher == publisher));

            using var context = new BooksContext();
            IEnumerable<Book> books = compiledQuery(context, "Wrox Press");

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }
            Console.WriteLine();
        }

        public static async Task CompileQueryAsync()
        {
            Console.WriteLine(nameof(CompileQueryAsync));
            Func<BooksContext, string, IAsyncEnumerable<Book>> compiledQuery = 
                EF.CompileAsyncQuery<BooksContext, string, Book>(
                    (context, publisher) 
                        => context.Books
                            .Where(b => b.Publisher == publisher));

            using var context = new BooksContext();
            IAsyncEnumerable<Book> books = compiledQuery(context, "Wrox Press");
            await foreach (var b in compiledQuery(context, "Wrox Press"))
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }

            Console.WriteLine();
        }
    }
}
