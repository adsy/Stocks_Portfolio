using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockAPI.Migrations
{
    public partial class SoldInstrumentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a6f140d-9637-4e7a-92c7-f2aa6028c395");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ad432acd-3f6c-42c8-a130-905b2bc2cbbd");

            migrationBuilder.CreateTable(
                name: "SoldInstruments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    PurchasePrice = table.Column<double>(type: "float", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    Profit = table.Column<double>(type: "float", nullable: false),
                    DiscountApplied = table.Column<bool>(type: "bit", nullable: false),
                    CGTPayable = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoldInstruments", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoldInstruments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0da80e0f-cc58-4bb6-a6dd-5d6233341759");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c77cd5fb-bb12-42f5-8bdb-edb3933d8acf");

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
    }
}
