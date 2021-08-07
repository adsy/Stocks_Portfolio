using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class addedExtraColumnsToSoldInstruments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0da80e0f-cc58-4bb6-a6dd-5d6233341759");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c77cd5fb-bb12-42f5-8bdb-edb3933d8acf");

            migrationBuilder.AddColumn<DateTime>(
                name: "PuchaseDate",
                table: "SoldInstruments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SaleDate",
                table: "SoldInstruments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ae0520d3-8631-448f-bc2d-262550f5c9b4", "e2eb2ac4-5587-4b2f-b7ff-d41f2b7a31b1", "User", "USER" },
                    { "c58a587f-6823-4c8a-87b3-f76458c4c355", "f62b7b86-3628-4731-96a2-c159953a8801", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2021, 8, 6, 12, 14, 26, 726, DateTimeKind.Local).AddTicks(2614));

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2021, 8, 6, 13, 14, 26, 726, DateTimeKind.Local).AddTicks(5537));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae0520d3-8631-448f-bc2d-262550f5c9b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c58a587f-6823-4c8a-87b3-f76458c4c355");

            migrationBuilder.DropColumn(
                name: "PuchaseDate",
                table: "SoldInstruments");

            migrationBuilder.DropColumn(
                name: "SaleDate",
                table: "SoldInstruments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c77cd5fb-bb12-42f5-8bdb-edb3933d8acf", "3f131f4c-ff09-4d77-b640-66ef5d5c415e", "User", "USER" },
                    { "0da80e0f-cc58-4bb6-a6dd-5d6233341759", "93a1237e-b054-4db0-a384-ee78463803e7", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2021, 8, 6, 11, 23, 17, 513, DateTimeKind.Local).AddTicks(4474));

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2021, 8, 6, 12, 23, 17, 513, DateTimeKind.Local).AddTicks(7464));
        }
    }
}
