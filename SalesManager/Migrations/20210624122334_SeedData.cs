using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesManager.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Prices",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "PaymentTypes",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemsID", "DateAdded", "Group", "ItemName", "MinimumStock" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(2494), "Consoles", "Playstation 2", 20 },
                    { 2, new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4808), "Consoles", "XBox One", 10 },
                    { 3, new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4818), "Consoles", "XBox 360", 15 },
                    { 4, new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4822), "Consoles", "XBox", 5 },
                    { 5, new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4825), "Contollers", "XBox One Wired Controller", 10 },
                    { 6, new DateTime(2021, 6, 24, 12, 23, 33, 869, DateTimeKind.Utc).AddTicks(4834), "Contollers", "XBox 360 Wireless Controller", 10 }
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "PaymentTypesID", "PaymentType" },
                values: new object[,]
                {
                    { (byte)1, "Cash" },
                    { (byte)2, "Mobile Money" },
                    { (byte)3, "Vodafone Cash" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypesID",
                keyValue: (byte)1);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypesID",
                keyValue: (byte)2);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "PaymentTypesID",
                keyValue: (byte)3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Prices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentType",
                table: "PaymentTypes",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);
        }
    }
}
