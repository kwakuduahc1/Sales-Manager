using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    public partial class SupPay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SupplierPayments",
                columns: table => new
                {
                    SupplierPaymentsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuppliersID = table.Column<int>(type: "int", nullable: false),
                    DatePaid = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    PaymentTypesID = table.Column<byte>(type: "tinyint", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierPayments", x => x.SupplierPaymentsID);
                    table.ForeignKey(
                        name: "FK_SupplierPayments_PaymentTypes_PaymentTypesID",
                        column: x => x.PaymentTypesID,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplierPayments_Suppliers_SuppliersID",
                        column: x => x.SuppliersID,
                        principalTable: "Suppliers",
                        principalColumn: "SuppliersID",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_PaymentTypesID",
                table: "SupplierPayments",
                column: "PaymentTypesID");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierPayments_SuppliersID",
                table: "SupplierPayments",
                column: "SuppliersID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupplierPayments");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 1,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4098));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 2,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4115));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 3,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4120));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 4,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4123));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 5,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4128));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemsID",
                keyValue: 6,
                column: "DateAdded",
                value: new DateTime(2023, 5, 9, 6, 7, 48, 918, DateTimeKind.Utc).AddTicks(4138));
        }
    }
}
