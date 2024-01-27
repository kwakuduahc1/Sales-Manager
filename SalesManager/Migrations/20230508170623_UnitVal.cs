using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    public partial class UnitVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Items_ItemsID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "PricesID",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "ItemsID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 6, 23, 268, DateTimeKind.Utc).AddTicks(962));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 6, 23, 268, DateTimeKind.Utc).AddTicks(977));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 6, 23, 268, DateTimeKind.Utc).AddTicks(982));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 6, 23, 268, DateTimeKind.Utc).AddTicks(985));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 6, 23, 268, DateTimeKind.Utc).AddTicks(989));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 6, 23, 268, DateTimeKind.Utc).AddTicks(998));

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Items_ItemsID",
                table: "Units",
                column: "ItemsID",
                principalTable: "Items",
                principalColumn: "ItemsID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Items_ItemsID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Units");

            migrationBuilder.AlterColumn<int>(
                name: "ItemsID",
                table: "Units",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PricesID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 16, 27, 24, 710, DateTimeKind.Utc).AddTicks(4350));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 16, 27, 24, 710, DateTimeKind.Utc).AddTicks(4361));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 16, 27, 24, 710, DateTimeKind.Utc).AddTicks(4365));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 16, 27, 24, 710, DateTimeKind.Utc).AddTicks(4368));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 16, 27, 24, 710, DateTimeKind.Utc).AddTicks(4371));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 16, 27, 24, 710, DateTimeKind.Utc).AddTicks(4377));

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Items_ItemsID",
                table: "Units",
                column: "ItemsID",
                principalTable: "Items",
                principalColumn: "ItemsID");
        }
    }
}
