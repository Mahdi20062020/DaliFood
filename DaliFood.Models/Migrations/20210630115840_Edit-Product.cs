using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaliFood.Models.Migrations
{
    public partial class EditProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Customer_CustomerId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Discount_ProductId",
                table: "Discount");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Product",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CustomersProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomersProduct_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomersProduct_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomersProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ProductId",
                table: "Discount",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersProduct_CustomerId",
                table: "CustomersProduct",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersProduct_DiscountId",
                table: "CustomersProduct",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersProduct_ProductId",
                table: "CustomersProduct",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Customer_CustomerId",
                table: "Product",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Customer_CustomerId",
                table: "Product");

            migrationBuilder.DropTable(
                name: "CustomersProduct");

            migrationBuilder.DropIndex(
                name: "IX_Discount_ProductId",
                table: "Discount");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Discount_ProductId",
                table: "Discount",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Customer_CustomerId",
                table: "Product",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
