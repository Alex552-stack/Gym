using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class GymVisits_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "470b2eb6-e03f-43c0-a065-78a861931196");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9df2e771-5a34-456c-b55f-bb9d797f1830");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "421edef9-a8a2-492a-a3be-f9cbd23b9f1c", null, "Member", "MEMBER" },
                    { "a90bae2b-e575-4374-bdf5-d8c26fc5b26d", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "421edef9-a8a2-492a-a3be-f9cbd23b9f1c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a90bae2b-e575-4374-bdf5-d8c26fc5b26d");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "470b2eb6-e03f-43c0-a065-78a861931196", null, "Admin", "ADMIN" },
                    { "9df2e771-5a34-456c-b55f-bb9d797f1830", null, "Member", "MEMBER" }
                });
        }
    }
}
