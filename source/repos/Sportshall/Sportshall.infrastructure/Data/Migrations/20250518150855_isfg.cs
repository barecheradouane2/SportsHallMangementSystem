using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportshall.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class isfg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Activities_ActivitiesID",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Products_ProductID",
                table: "Photo");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Activities_ActivitiesID",
                table: "Photo",
                column: "ActivitiesID",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Products_ProductID",
                table: "Photo",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Activities_ActivitiesID",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Products_ProductID",
                table: "Photo");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Activities_ActivitiesID",
                table: "Photo",
                column: "ActivitiesID",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Products_ProductID",
                table: "Photo",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
