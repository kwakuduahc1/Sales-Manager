using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    public partial class Supps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Stockings");

            migrationBuilder.AddColumn<int>(
                name: "SuppliersID",
                table: "Stockings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SuppliersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SuppliersID);
                });

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7746));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7770));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7781));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7789));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7795));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 22, 10, 32, 361, DateTimeKind.Utc).AddTicks(7805));

            migrationBuilder.CreateIndex(
                name: "IX_Stockings_SuppliersID",
                table: "Stockings",
                column: "SuppliersID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stockings_Suppliers_SuppliersID",
                table: "Stockings",
                column: "SuppliersID",
                principalTable: "Suppliers",
                principalColumn: "SuppliersID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stockings_Suppliers_SuppliersID",
                table: "Stockings");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Stockings_SuppliersID",
                table: "Stockings");

            migrationBuilder.DropColumn(
                name: "SuppliersID",
                table: "Stockings");

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Stockings",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 25, 33, 248, DateTimeKind.Utc).AddTicks(7897));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 25, 33, 248, DateTimeKind.Utc).AddTicks(7907));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 25, 33, 248, DateTimeKind.Utc).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 25, 33, 248, DateTimeKind.Utc).AddTicks(7913));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 25, 33, 248, DateTimeKind.Utc).AddTicks(7916));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 8, 17, 25, 33, 248, DateTimeKind.Utc).AddTicks(7922));
        }
    }
}
