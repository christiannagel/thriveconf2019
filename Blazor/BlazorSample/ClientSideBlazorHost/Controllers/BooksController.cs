using BooksLib;
using ClientSideBlazorHost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ClientSideBlazorHost.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _booksContext;

        public BooksController(BooksContext booksContext)
            => _booksContext = booksContext;

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return _booksContext.Books.ToList();
        }
    }
}
