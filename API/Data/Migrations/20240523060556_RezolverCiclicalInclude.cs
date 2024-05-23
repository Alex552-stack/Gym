using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class RezolverCiclicalInclude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "6bb7393b-af82-45b7-a869-375dfbb230b0");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ae7d8bba-b1f1-4d6e-a1c9-f30c506a9329");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "adaecb7f-e4ec-42b1-9409-70234c0e4ea3", null, "Admin", "ADMIN" },
                    { "b605dc64-7844-4c36-91f7-8235a06a6f27", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "6bb7393b-af82-45b7-a869-375dfbb230b0", null, "Admin", "ADMIN" },
                    { "ae7d8bba-b1f1-4d6e-a1c9-f30c506a9329", null, "Member", "MEMBER" }
                });
        }
    }
}
