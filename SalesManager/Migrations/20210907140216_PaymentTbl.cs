using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class PaymentTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanContact",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Cash",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Customer",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "MobileMoney",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "Receipt",
                table: "Sales",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentsReceipt",
                table: "Sales",
                type: "nvarchar(20)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Receipt = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cash = table.Column<decimal>(type: "money", nullable: false),
                    MobileMoney = table.Column<decimal>(type: "money", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CanContact = table.Column<bool>(type: "bit", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Receipt);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 2, 16, 25, DateTimeKind.Utc).AddTicks(4254));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 2, 16, 25, DateTimeKind.Utc).AddTicks(8320));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 2, 16, 25, DateTimeKind.Utc).AddTicks(8341));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 2, 16, 25, DateTimeKind.Utc).AddTicks(8351));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 2, 16, 25, DateTimeKind.Utc).AddTicks(8358));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 2, 16, 25, DateTimeKind.Utc).AddTicks(8372));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PaymentsReceipt",
                table: "Sales",
                column: "PaymentsReceipt");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Payments_PaymentsReceipt",
                table: "Sales",
                column: "PaymentsReceipt",
                principalTable: "Payments",
                principalColumn: "Receipt",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Payments_PaymentsReceipt",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PaymentsReceipt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentsReceipt",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "Receipt",
                table: "Sales",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<bool>(
                name: "CanContact",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Cash",
                table: "Sales",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Customer",
                table: "Sales",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "MobileMoney",
                table: "Sales",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Sales",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 13, 15, 55, 936, DateTimeKind.Utc).AddTicks(8018));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 13, 15, 55, 937, DateTimeKind.Utc).AddTicks(133));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 13, 15, 55, 937, DateTimeKind.Utc).AddTicks(141));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 13, 15, 55, 937, DateTimeKind.Utc).AddTicks(145));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 13, 15, 55, 937, DateTimeKind.Utc).AddTicks(149));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 13, 15, 55, 937, DateTimeKind.Utc).AddTicks(157));
        }
    }
}
