using Microsoft.EntityFrameworkCore;

namespace TableSplitting
{
    public static class SchemaNames
    {
        public const string Menus = nameof(Menus);
    }

    public class MenusContext : DbContext
    {
        private const string ConnectionString = @"server=(localdb)\MSSQLLocalDb;" +
            "Database=ThriveMenus;Trusted_Connection=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>().HasOne<MenuDetails>("Details").WithOne("Menu").HasForeignKey<MenuDetails>(d => d.MenuDetailsId);
            modelBuilder.Entity<Menu>().ToTable(SchemaNames.Menus);
            modelBuilder.Entity<MenuDetails>().ToTable(SchemaNames.Menus);
            modelBuilder.Entity<Menu>().Property(m => m.Price).HasColumnType("money");
        }

        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<MenuDetails> MenuDetails { get; set; } = default!;
    }
}
