using System.Collections.Generic;

namespace BooksSample
{
    public class Book
    {
        public Book(string title, string publisher)
            => (Title, _publisher) = (title, publisher);

        private int _bookId = 0;
        public int BookId => _bookId;
        public string Title { get; set; }
        private string _publisher;
        public string Publisher => _publisher;

        private HashSet<BookAuthor> _bookAuthors = new HashSet<BookAuthor>();

        public virtual ICollection<BookAuthor> BookAuthors => _bookAuthors;

        public override string ToString() => $"id: {_bookId}, title: {Title}, publisher: {Publisher}";

        private static Book? s_Book;
        public static Book Emtpy => s_Book ??= new Book(string.Empty, string.Empty);
    }
}
