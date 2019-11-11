using System.Collections.Generic;

namespace BooksSample
{
    public class Author
    {
        public Author(string firstName, string lastName)
            => (_firstName, _lastName) = (firstName, lastName);

        private static Author? _empty;
        public static Author Empty => _empty ??= new Author(string.Empty, string.Empty);

        private int _authorId = 0;
        public int AuthorId => _authorId;

        private string _firstName;
        public string FirstName => _firstName;
        private string _lastName;
        public string LastName => _lastName;

        public virtual ICollection<BookAuthor> BookAuthors { get; } = new HashSet<BookAuthor>();

        public override string ToString() => $"{FirstName} {LastName}";
    }
}
