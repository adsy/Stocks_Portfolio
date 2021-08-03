using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class AddRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "ad432acd-3f6c-42c8-a130-905b2bc2cbbd", "cc83a92a-3ec5-4a9e-8016-4601497fcb54", "User", "USER" },
                    { "6a6f140d-9637-4e7a-92c7-f2aa6028c395", "37d8826d-8a9a-40d8-bd82-ff85af3c243d", "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeStamp",
                value: new DateTime(2021, 8, 3, 16, 19, 46, 823, DateTimeKind.Local).AddTicks(3424));

            migrationBuilder.UpdateData(
                table: "PortfolioTrackers",
                keyColumn: "Id",
                keyValue: 2,
                column: "TimeStamp",
                value: new DateTime(2021, 8, 3, 17, 19, 46, 823, DateTimeKind.Local).AddTicks(6383));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a6f140d-9637-4e7a-92c7-f2aa6028c395");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad432acd-3f6c-42c8-a130-905b2bc2cbbd");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

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
    }
}