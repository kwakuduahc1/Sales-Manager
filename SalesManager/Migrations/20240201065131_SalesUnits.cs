using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    /// <inheritdoc />
    public partial class SalesUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitsID",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 51, 29, 107, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 51, 29, 107, DateTimeKind.Utc).AddTicks(2358));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 51, 29, 107, DateTimeKind.Utc).AddTicks(2367));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 51, 29, 107, DateTimeKind.Utc).AddTicks(2373));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 51, 29, 107, DateTimeKind.Utc).AddTicks(2379));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 51, 29, 107, DateTimeKind.Utc).AddTicks(2399));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsID",
                table: "Sales");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 32, 3, 934, DateTimeKind.Utc).AddTicks(4287));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 32, 3, 934, DateTimeKind.Utc).AddTicks(4306));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 32, 3, 934, DateTimeKind.Utc).AddTicks(4313));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 32, 3, 934, DateTimeKind.Utc).AddTicks(4320));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 32, 3, 934, DateTimeKind.Utc).AddTicks(4327));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 2, 1, 6, 32, 3, 934, DateTimeKind.Utc).AddTicks(4348));
        }
    }
}
