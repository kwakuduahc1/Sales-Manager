using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class PaymentOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cash",
                table: "Sales",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "MobileMoney",
                table: "Sales",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 12, 43, 42, 104, DateTimeKind.Utc).AddTicks(7272));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 12, 43, 42, 104, DateTimeKind.Utc).AddTicks(9324));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 12, 43, 42, 104, DateTimeKind.Utc).AddTicks(9332));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 12, 43, 42, 104, DateTimeKind.Utc).AddTicks(9336));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 12, 43, 42, 104, DateTimeKind.Utc).AddTicks(9339));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 9, 7, 12, 43, 42, 104, DateTimeKind.Utc).AddTicks(9347));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cash",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "MobileMoney",
                table: "Sales");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 8, 28, 16, 17, 12, 656, DateTimeKind.Utc).AddTicks(6300));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 8, 28, 16, 17, 12, 656, DateTimeKind.Utc).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 8, 28, 16, 17, 12, 656, DateTimeKind.Utc).AddTicks(8230));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 8, 28, 16, 17, 12, 656, DateTimeKind.Utc).AddTicks(8233));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 8, 28, 16, 17, 12, 656, DateTimeKind.Utc).AddTicks(8236));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 8, 28, 16, 17, 12, 656, DateTimeKind.Utc).AddTicks(8245));
        }
    }
}
