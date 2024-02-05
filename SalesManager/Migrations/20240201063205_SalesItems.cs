using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    /// <inheritdoc />
    public partial class SalesItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 21, 12, 46, 913, DateTimeKind.Utc).AddTicks(2865));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 21, 12, 46, 913, DateTimeKind.Utc).AddTicks(2875));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 21, 12, 46, 913, DateTimeKind.Utc).AddTicks(2879));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 21, 12, 46, 913, DateTimeKind.Utc).AddTicks(2882));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 21, 12, 46, 913, DateTimeKind.Utc).AddTicks(2885));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 1, 26, 21, 12, 46, 913, DateTimeKind.Utc).AddTicks(2893));
        }
    }
}
