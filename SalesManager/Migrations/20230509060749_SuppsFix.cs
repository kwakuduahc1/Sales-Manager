using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    public partial class SuppsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SupplierName",
                table: "Suppliers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4115));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4138));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupplierName",
                table: "Suppliers");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7746));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7770));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7781));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7789));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7805));
        }
    }
}
