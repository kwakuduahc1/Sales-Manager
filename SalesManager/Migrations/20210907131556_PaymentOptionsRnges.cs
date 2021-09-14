using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class PaymentOptionsRnges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
