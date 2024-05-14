using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace User.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToUserDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bde038e9-6337-416a-9920-0709e6e2bbd8", null, "Admin", "ADMIN" },
                    { "f7bb14fb-5fe4-4c7b-b5d1-982ba6c216bf", null, "Employee", "EMPLOYEE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bde038e9-6337-416a-9920-0709e6e2bbd8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7bb14fb-5fe4-4c7b-b5d1-982ba6c216bf");
        }
    }
}
