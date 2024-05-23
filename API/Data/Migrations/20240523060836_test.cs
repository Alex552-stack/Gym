using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "adaecb7f-e4ec-42b1-9409-70234c0e4ea3");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b605dc64-7844-4c36-91f7-8235a06a6f27");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f1098b3-11f4-49b7-a2f6-be6abc7b5ce2", null, "Member", "MEMBER" },
                    { "65f1638e-3c17-4f2e-9cc2-5698c24c9f0b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4f1098b3-11f4-49b7-a2f6-be6abc7b5ce2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "65f1638e-3c17-4f2e-9cc2-5698c24c9f0b");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "adaecb7f-e4ec-42b1-9409-70234c0e4ea3", null, "Admin", "ADMIN" },
                    { "b605dc64-7844-4c36-91f7-8235a06a6f27", null, "Member", "MEMBER" }
                });
        }
    }
}
