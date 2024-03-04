using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthApp.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ae19e72-0831-4946-a087-753c78fa9697", "6799dda5-334e-4d35-b6e4-24b0bfff272e", "User", "USER" },
                    { "c4deeb2b-23f9-45a7-a768-17e9ff12b667", "71d90463-31ce-4f1a-b37d-fadf8882de65", "Admin", "ADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ae19e72-0831-4946-a087-753c78fa9697");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4deeb2b-23f9-45a7-a768-17e9ff12b667");
        }
    }
}
