using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManager.Migrations
{
    /// <inheritdoc />
    public partial class Exps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenditureItems",
                columns: table => new
                {
                    ExpenditureItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenditureItems", x => x.ExpenditureItemsID);
                });

            migrationBuilder.CreateTable(
                name: "Expenditures",
                columns: table => new
                {
                    ExpenditureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenditureItemsID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenditures", x => x.ExpenditureID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenditureItems");

            migrationBuilder.DropTable(
                name: "Expenditures");
        }
    }
}
