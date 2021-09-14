using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class FixFKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Payments_PaymentsReceipt",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PaymentsReceipt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentsReceipt",
                table: "Sales");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 28, 9, 410, DateTimeKind.Utc).AddTicks(1620));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 28, 9, 410, DateTimeKind.Utc).AddTicks(5929));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 28, 9, 410, DateTimeKind.Utc).AddTicks(5948));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 28, 9, 410, DateTimeKind.Utc).AddTicks(5953));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 28, 9, 410, DateTimeKind.Utc).AddTicks(5958));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 14, 28, 9, 410, DateTimeKind.Utc).AddTicks(5969));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Receipt",
                table: "Sales",
                column: "Receipt");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Payments_Receipt",
                table: "Sales",
                column: "Receipt",
                principalTable: "Payments",
                principalColumn: "Receipt",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Payments_Receipt",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_Receipt",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "PaymentsReceipt",
                table: "Sales",
                type: "nvarchar(20)",
                nullable: true);

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
    }
}
