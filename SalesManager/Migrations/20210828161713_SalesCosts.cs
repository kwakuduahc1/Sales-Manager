using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class SalesCosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Sales",
                type: "money",
                nullable: false,
                defaultValue: 0m);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Sales");

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
    }
}
