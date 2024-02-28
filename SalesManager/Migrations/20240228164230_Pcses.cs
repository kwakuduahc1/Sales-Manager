using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    /// <inheritdoc />
    public partial class Pcses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsID",
                table: "Sales");

            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "Suppliers",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Items",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2024, 2, 28, 16, 42, 28, 712, DateTimeKind.Utc).AddTicks(8666));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2024, 2, 28, 16, 42, 28, 712, DateTimeKind.Utc).AddTicks(8675));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2024, 2, 28, 16, 42, 28, 712, DateTimeKind.Utc).AddTicks(8680));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2024, 2, 28, 16, 42, 28, 712, DateTimeKind.Utc).AddTicks(8683));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2024, 2, 28, 16, 42, 28, 712, DateTimeKind.Utc).AddTicks(8686));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2024, 2, 28, 16, 42, 28, 712, DateTimeKind.Utc).AddTicks(8693));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SupplierName",
                table: "Suppliers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AddColumn<int>(
                name: "UnitsID",
                table: "Sales",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemName",
                table: "Items",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

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
    }
}
