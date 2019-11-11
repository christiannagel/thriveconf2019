using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationsLib.Migrations
{
    public partial class AddAllergens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Allergens",
                schema: "mc",
                table: "Menus",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Allergens",
                schema: "mc",
                table: "Menus");
        }
    }
}
