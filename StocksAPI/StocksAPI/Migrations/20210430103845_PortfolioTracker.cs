using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class PortfolioTracker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5a7e953-54be-4c85-b14f-2166e46b037d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa8b429c-5615-42ee-ab82-e801e399e478");

            migrationBuilder.CreateTable(
                name: "PortfolioTrackers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PortfolioTotal = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioTrackers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b0450b61-ca98-4b82-8b60-c8215d28c460", "b56b32d3-5916-4f05-85b6-b778c3a6f366", "User", "USER" },
                    { "11def3e3-f491-4e51-8ecc-e026f1019ca2", "9ef97f61-9da1-4e62-9750-918ab564a8cf", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "PortfolioTrackers",
                columns: new[] { "Id", "PortfolioTotal", "TimeStamp" },
                values: new object[,]
                {
                    { 1, 2000.0, new DateTime(2021, 4, 30, 19, 38, 44, 980, DateTimeKind.Local).AddTicks(2332) },
                    { 2, 2000.0, new DateTime(2021, 4, 30, 20, 38, 44, 980, DateTimeKind.Local).AddTicks(5038) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioTrackers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11def3e3-f491-4e51-8ecc-e026f1019ca2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b0450b61-ca98-4b82-8b60-c8215d28c460");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c5a7e953-54be-4c85-b14f-2166e46b037d", "76b84e81-57a7-44fe-b361-c20285b4dc39", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa8b429c-5615-42ee-ab82-e801e399e478", "030702e2-3343-47ba-843e-0123a8826529", "Admin", "ADMIN" });
        }
    }
}