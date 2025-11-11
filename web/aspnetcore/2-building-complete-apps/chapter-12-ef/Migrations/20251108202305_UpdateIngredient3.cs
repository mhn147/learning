using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chapter_12_ef.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIngredient3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVegan",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "Ingredient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVegan",
                table: "Ingredient",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "Ingredient",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
