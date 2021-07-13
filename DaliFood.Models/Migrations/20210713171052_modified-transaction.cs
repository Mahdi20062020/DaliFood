using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DaliFood.Models.Migrations
{
    public partial class modifiedtransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "Deposit");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Withdraw",
                table: "Withdraw");

            migrationBuilder.RenameTable(
                name: "Withdraw",
                newName: "TransactionItem");

            migrationBuilder.AlterColumn<int>(
                name: "OldAmount",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NewAmount",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Transaction",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TrackingCode",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "TransactionItem",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Shabanumber",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepositDate",
                table: "TransactionItem",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Cardnumber",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deposit_Status",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deposit_TransactionId",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaleReferenceId",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Withdraw_Cardnumber",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Withdraw_DepositDate",
                table: "TransactionItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Withdraw_Status",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Withdraw_TransactionId",
                table: "TransactionItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItem",
                table: "TransactionItem",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ItemId",
                table: "Transaction",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_Deposit_TransactionId",
                table: "TransactionItem",
                column: "Deposit_TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_TransactionId",
                table: "TransactionItem",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItem_Withdraw_TransactionId",
                table: "TransactionItem",
                column: "Withdraw_TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_TransactionItem_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "TransactionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionItem_ItemId",
                table: "Transaction",
                column: "ItemId",
                principalTable: "TransactionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Transaction_Deposit_TransactionId",
                table: "TransactionItem",
                column: "Deposit_TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Transaction_TransactionId",
                table: "TransactionItem",
                column: "TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Transaction_Withdraw_TransactionId",
                table: "TransactionItem",
                column: "Withdraw_TransactionId",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_TransactionItem_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionItem_ItemId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Transaction_Deposit_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Transaction_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Transaction_Withdraw_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_ItemId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItem",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItem_Deposit_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItem_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItem_Withdraw_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Deposit_Status",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Deposit_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "RefId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "SaleReferenceId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Withdraw_Cardnumber",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Withdraw_DepositDate",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Withdraw_Status",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "Withdraw_TransactionId",
                table: "TransactionItem");

            migrationBuilder.RenameTable(
                name: "TransactionItem",
                newName: "Withdraw");

            migrationBuilder.AlterColumn<string>(
                name: "OldAmount",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NewAmount",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Amount",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingCode",
                table: "Withdraw",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Withdraw",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Shabanumber",
                table: "Withdraw",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepositDate",
                table: "Withdraw",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cardnumber",
                table: "Withdraw",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Withdraw",
                table: "Withdraw",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Deposit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    Cardnumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepositDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaleReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
