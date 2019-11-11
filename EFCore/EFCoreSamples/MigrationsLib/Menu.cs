namespace MigrationsLib
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Text { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Allergens { get; set; }
        public int MenuCardId { get; set; }
        public MenuCard MenuCard { get; set; } = default!;
        public override string ToString() => Text;
    }
}
