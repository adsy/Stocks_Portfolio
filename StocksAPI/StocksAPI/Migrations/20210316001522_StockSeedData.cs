using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class StockSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3863ccac-ccec-4b26-a1e6-6910de077dac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3fd102d-b1d5-400a-a095-03e401bf9897");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c5a7e953-54be-4c85-b14f-2166e46b037d", "76b84e81-57a7-44fe-b361-c20285b4dc39", "User", "USER" },
                    { "fa8b429c-5615-42ee-ab82-e801e399e478", "030702e2-3343-47ba-843e-0123a8826529", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amount", "Country", "Name", "PurchaseDate", "PurchasePrice", "TotalCost" },
                values: new object[,]
                {
                    { 1, 15.0, "US", "BB", new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14.0, 210.0 },
                    { 2, 214.6241, "US", "SENS", new DateTime(2021, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.6499999999999999, 569.83000000000004 },
                    { 3, 3517.0, "AU", "LOT", new DateTime(2021, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.14799999999999999, 499.97000000000003 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5a7e953-54be-4c85-b14f-2166e46b037d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa8b429c-5615-42ee-ab82-e801e399e478");

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stocks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3863ccac-ccec-4b26-a1e6-6910de077dac", "630b8f87-e4f1-4e62-894b-69b44057b4b6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b3fd102d-b1d5-400a-a095-03e401bf9897", "bd6c8ff0-1b0d-489c-bc0c-71cf2fa7dd9e", "Admin", "ADMIN" });
        }
    }
}
