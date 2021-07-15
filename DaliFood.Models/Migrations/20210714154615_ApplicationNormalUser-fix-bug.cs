using Microsoft.EntityFrameworkCore.Migrations;

namespace DaliFood.Models.Migrations
{
    public partial class ApplicationNormalUserfixbug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "ApplicationUserDetail");

            migrationBuilder.AlterColumn<int>(
                name: "Wallet",
                table: "ApplicationUserDetail",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationNormalUserUserId",
                table: "Address",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_ApplicationNormalUserUserId",
                table: "Address",
                column: "ApplicationNormalUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_ApplicationUserDetail_ApplicationNormalUserUserId",
                table: "Address",
                column: "ApplicationNormalUserUserId",
                principalTable: "ApplicationUserDetail",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_ApplicationUserDetail_ApplicationNormalUserUserId",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ApplicationNormalUserUserId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ApplicationNormalUserUserId",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Wallet",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "ApplicationUserDetail",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);
        }
    }
}
