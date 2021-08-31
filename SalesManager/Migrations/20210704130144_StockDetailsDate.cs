using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class StockDetailsDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateBought",
                table: "Stockings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 13, 1, 43, 569, DateTimeKind.Utc).AddTicks(8576));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 13, 1, 43, 570, DateTimeKind.Utc).AddTicks(1849));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 13, 1, 43, 570, DateTimeKind.Utc).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 13, 1, 43, 570, DateTimeKind.Utc).AddTicks(1875));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 13, 1, 43, 570, DateTimeKind.Utc).AddTicks(1884));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 13, 1, 43, 570, DateTimeKind.Utc).AddTicks(1900));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBought",
                table: "Stockings");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 12, 57, 53, 576, DateTimeKind.Utc).AddTicks(7693));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 12, 57, 53, 576, DateTimeKind.Utc).AddTicks(9579));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 12, 57, 53, 576, DateTimeKind.Utc).AddTicks(9587));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 12, 57, 53, 576, DateTimeKind.Utc).AddTicks(9590));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 12, 57, 53, 576, DateTimeKind.Utc).AddTicks(9593));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 7, 4, 12, 57, 53, 576, DateTimeKind.Utc).AddTicks(9600));
        }
    }
}
