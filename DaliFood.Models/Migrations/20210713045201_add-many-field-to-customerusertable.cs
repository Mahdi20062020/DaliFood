using Microsoft.EntityFrameworkCore.Migrations;

namespace DaliFood.Models.Migrations
{
    public partial class addmanyfieldtocustomerusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerFamily",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phonenumber",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelePhonenumber1",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelePhonenumber2",
                table: "ApplicationUserDetail",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "OwnerFamily",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "Phonenumber",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "TelePhonenumber1",
                table: "ApplicationUserDetail");

            migrationBuilder.DropColumn(
                name: "TelePhonenumber2",
                table: "ApplicationUserDetail");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Address",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
