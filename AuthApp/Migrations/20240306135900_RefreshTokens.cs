using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthApp.Migrations
{
    public partial class RefreshTokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ae19e72-0831-4946-a087-753c78fa9697");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4deeb2b-23f9-45a7-a768-17e9ff12b667");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenCreated",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0a35f6ba-98e8-4a64-a260-10d228301f3f", "514f2c3a-64a1-445d-81b6-79eda46e1da1", "Admin", "ADMIN" },
                    { "b7b80c07-74a3-4b22-9947-f12ead69bc0e", "da120f83-62e2-4e52-a016-5a79c70ad17e", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0a35f6ba-98e8-4a64-a260-10d228301f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7b80c07-74a3-4b22-9947-f12ead69bc0e");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ae19e72-0831-4946-a087-753c78fa9697", "6799dda5-334e-4d35-b6e4-24b0bfff272e", "User", "USER" },
                    { "c4deeb2b-23f9-45a7-a768-17e9ff12b667", "71d90463-31ce-4f1a-b37d-fadf8882de65", "Admin", "ADMIN" }
                });
        }
    }
}
