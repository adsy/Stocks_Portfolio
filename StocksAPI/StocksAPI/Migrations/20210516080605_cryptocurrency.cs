using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class cryptocurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11def3e3-f491-4e51-8ecc-e026f1019ca2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0450b61-ca98-4b82-8b60-c8215d28c460");

            migrationBuilder.CreateTable(
                name: "Cryptocurrencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    TotalCost = table.Column<double>(type: "float", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cryptocurrencies", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b00fa05a-11c2-4ab9-9fc3-5570ac1120b4", "31a88e92-ec6c-4c01-91dd-4a9e9a435416", "User", "USER" },
                    { "9a0c4e8c-da6c-4039-a170-1bb490d90ed7", "1b9529d8-acaa-44c7-b443-e84b9e73fb14", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2021, 5, 16, 17, 6, 5, 104, DateTimeKind.Local).AddTicks(6463));

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2021, 5, 16, 18, 6, 5, 104, DateTimeKind.Local).AddTicks(9226));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cryptocurrencies");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a0c4e8c-da6c-4039-a170-1bb490d90ed7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b00fa05a-11c2-4ab9-9fc3-5570ac1120b4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b0450b61-ca98-4b82-8b60-c8215d28c460", "b56b32d3-5916-4f05-85b6-b778c3a6f366", "User", "USER" },
                    { "11def3e3-f491-4e51-8ecc-e026f1019ca2", "9ef97f61-9da1-4e62-9750-918ab564a8cf", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2021, 4, 30, 19, 38, 44, 980, DateTimeKind.Local).AddTicks(2332));

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2021, 4, 30, 20, 38, 44, 980, DateTimeKind.Local).AddTicks(5038));
        }
    }
}
