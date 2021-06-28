using Microsoft.EntityFrameworkCore.Migrations;

namespace DaliFood.Models.Migrations
{
    public partial class addnavigtions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCategorieId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategorieId",
                table: "Product",
                column: "ProductCategorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_RestaurantId",
                table: "Product",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductCategorie_ProductCategorieId",
                table: "Product",
                column: "ProductCategorieId",
                principalTable: "ProductCategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Restaurant_RestaurantId",
                table: "Product",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductCategorie_ProductCategorieId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Restaurant_RestaurantId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_ProductCategorieId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_RestaurantId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductCategorieId",
                table: "Product");
        }
    }
}
