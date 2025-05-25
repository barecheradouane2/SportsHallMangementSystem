using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportshall.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class kgnk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSales_Products_ProductsID",
                table: "ProductSales");

            migrationBuilder.DropIndex(
                name: "IX_ProductSales_ProductsID",
                table: "ProductSales");

            migrationBuilder.DropColumn(
                name: "ProductsID",
                table: "ProductSales");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "ProductSales");

            migrationBuilder.CreateTable(
                name: "ProductSalesItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductSalesID = table.Column<int>(type: "int", nullable: false),
                    ProductsID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    qty = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSalesItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSalesItems_ProductSales_ProductSalesID",
                        column: x => x.ProductSalesID,
                        principalTable: "ProductSales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSalesItems_ProductSalesID",
                table: "ProductSalesItems",
                column: "ProductSalesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSalesItems");

            migrationBuilder.AddColumn<int>(
                name: "ProductsID",
                table: "ProductSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "ProductSales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductsID",
                table: "ProductSales",
                column: "ProductsID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSales_Products_ProductsID",
                table: "ProductSales",
                column: "ProductsID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
