using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    public partial class PayPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Items_ItemsID",
                table: "Sales");

            migrationBuilder.AlterColumn<int>(
                name: "ItemsID",
                table: "Sales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PricesID",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 10, 6, 35, 0, 623, DateTimeKind.Utc).AddTicks(7488));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 10, 6, 35, 0, 623, DateTimeKind.Utc).AddTicks(7502));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 10, 6, 35, 0, 623, DateTimeKind.Utc).AddTicks(7507));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 10, 6, 35, 0, 623, DateTimeKind.Utc).AddTicks(7510));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 10, 6, 35, 0, 623, DateTimeKind.Utc).AddTicks(7514));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 10, 6, 35, 0, 623, DateTimeKind.Utc).AddTicks(7521));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PricesID",
                table: "Sales",
                column: "PricesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Items_ItemsID",
                table: "Sales",
                column: "ItemsID",
                principalTable: "Items",
                principalColumn: "ItemsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Prices_PricesID",
                table: "Sales",
                column: "PricesID",
                principalTable: "Prices",
                principalColumn: "PricesID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Items_ItemsID",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Prices_PricesID",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_PricesID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PricesID",
                table: "Sales");

            migrationBuilder.AlterColumn<int>(
                name: "ItemsID",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 15, 44, 5, 69, DateTimeKind.Utc).AddTicks(6924));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 15, 44, 5, 69, DateTimeKind.Utc).AddTicks(6933));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 15, 44, 5, 69, DateTimeKind.Utc).AddTicks(6936));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 15, 44, 5, 69, DateTimeKind.Utc).AddTicks(6938));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 15, 44, 5, 69, DateTimeKind.Utc).AddTicks(6941));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 15, 44, 5, 69, DateTimeKind.Utc).AddTicks(6948));

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Items_ItemsID",
                table: "Sales",
                column: "ItemsID",
                principalTable: "Items",
                principalColumn: "ItemsID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
