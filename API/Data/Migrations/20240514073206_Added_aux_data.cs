using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_aux_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "68fabf50-151d-40b2-a874-8c78beb12d62");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9f95b0d8-3c17-4d1b-ab9c-e2b1b1005d07");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01e7d478-1d6e-4dbf-a264-776776c3082e", null, "Member", "MEMBER" },
                    { "5b89adf9-9c2b-45e7-872f-32b8de13d40c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "01e7d478-1d6e-4dbf-a264-776776c3082e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5b89adf9-9c2b-45e7-872f-32b8de13d40c");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "68fabf50-151d-40b2-a874-8c78beb12d62", null, "Member", "MEMBER" },
                    { "9f95b0d8-3c17-4d1b-ab9c-e2b1b1005d07", null, "Admin", "ADMIN" }
                });
        }
    }
}
