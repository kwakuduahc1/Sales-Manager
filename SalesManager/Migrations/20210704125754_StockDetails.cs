using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class StockDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Receipt",
                table: "Stockings",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Receipt",
                table: "Stockings");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(2494));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4808));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4818));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4822));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4825));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4834));
        }
    }
}
