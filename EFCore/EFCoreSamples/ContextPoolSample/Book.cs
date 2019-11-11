using System.ComponentModel.DataAnnotations.Schema;

namespace ContextPoolSample
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
    }
}
