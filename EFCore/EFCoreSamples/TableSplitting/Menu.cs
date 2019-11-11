namespace TableSplitting
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public decimal Price { get; set; }
        public MenuDetails? Details { get; set; }
    }
}
