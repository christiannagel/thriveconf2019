namespace BooksSample
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = Book.Emtpy;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = Author.Empty;
    }
}
