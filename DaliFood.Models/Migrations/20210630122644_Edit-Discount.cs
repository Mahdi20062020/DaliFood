using Microsoft.EntityFrameworkCore.Migrations;

namespace DaliFood.Models.Migrations
{
    public partial class EditDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersProduct_Discount_DiscountId",
                table: "CustomersProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Product_ProductId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_ProductId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_CustomersProduct_DiscountId",
                table: "CustomersProduct");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "CustomersProduct");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Discount",
                newName: "CustomersProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_CustomersProductId",
                table: "Discount",
                column: "CustomersProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_CustomersProduct_CustomersProductId",
                table: "Discount",
                column: "CustomersProductId",
                principalTable: "CustomersProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discount_CustomersProduct_CustomersProductId",
                table: "Discount");

            migrationBuilder.DropIndex(
                name: "IX_Discount_CustomersProductId",
                table: "Discount");

            migrationBuilder.RenameColumn(
                name: "CustomersProductId",
                table: "Discount",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "CustomersProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ProductId",
                table: "Discount",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersProduct_DiscountId",
                table: "CustomersProduct",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersProduct_Discount_DiscountId",
                table: "CustomersProduct",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Product_ProductId",
                table: "Discount",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
