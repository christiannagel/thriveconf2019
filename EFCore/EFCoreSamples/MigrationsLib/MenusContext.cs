using Microsoft.EntityFrameworkCore;

namespace MigrationsLib
{
    public class MenusContext : DbContext
    {
        public MenusContext(DbContextOptions<MenusContext> options): 
            base(options) { }

        public DbSet<Menu> Menus { get; set; } = default!;
        public DbSet<MenuCard> MenuCards { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mc");

            modelBuilder.ApplyConfiguration(new MenuCardConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
        }
    }
}
