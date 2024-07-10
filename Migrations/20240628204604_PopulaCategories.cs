using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categories(Name, UrlImage) Values('Bebidas', 'bebidas.jpg')");
            migrationBuilder.Sql("Insert into Categories(Name, UrlImage) Values('Lanches', 'lanches.jpg')");
            migrationBuilder.Sql("Insert into Categories(Name, UrlImage) Values('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categories");
        }
    }
}
