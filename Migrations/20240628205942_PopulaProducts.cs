using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Product(Name, Description, Price, UrlImage, Stock, DateRegister, CategoryId)" +
                "Values('Coca-cola Coffee', 'Refrigerante de Cola com extrato de café 350 ml', 3.99, 'cocacolacoffee.jpg', 50, now(), 1)");
            migrationBuilder.Sql("Insert into Product(Name, Description, Price, UrlImage, Stock, DateRegister, CategoryId)" +
                "Values('Lanche de Frango', 'Lanche de frango com creamcheese', 9.30, 'lanchefrangocreamcheese.jpg', 15, now(), 2)");
            migrationBuilder.Sql("Insert into Product(Name, Description, Price, UrlImage, Stock, DateRegister, CategoryId)" +
                "Values('Brownie', 'Bolo de chocolate', 4.50, 'brownie.jpg', 20, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Product");
        }
    }
}
