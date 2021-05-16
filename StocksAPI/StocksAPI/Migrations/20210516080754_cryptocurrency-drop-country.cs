using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class cryptocurrencydropcountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a0c4e8c-da6c-4039-a170-1bb490d90ed7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b00fa05a-11c2-4ab9-9fc3-5570ac1120b4");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Cryptocurrencies");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4eed6c2a-d8c2-41df-8940-3252c85e4255", "5e7c7b41-2ebf-4cc9-8ef4-1afd86802e01", "User", "USER" },
                    { "96b3abd1-56d3-4980-83d0-55469fa4bf23", "e042e2df-45fd-4eee-8a5b-a4030f31e592", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2021, 5, 16, 17, 7, 54, 87, DateTimeKind.Local).AddTicks(9382));

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2021, 5, 16, 18, 7, 54, 88, DateTimeKind.Local).AddTicks(3928));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4eed6c2a-d8c2-41df-8940-3252c85e4255");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96b3abd1-56d3-4980-83d0-55469fa4bf23");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Cryptocurrencies",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
