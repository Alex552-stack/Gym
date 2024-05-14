using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class third_try : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5f947ebb-298f-4fc3-8d42-90eca6f29531");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8fbfb03f-bda4-45b7-8b9a-d8e798039af9");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "AuxDatas",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "470b2eb6-e03f-43c0-a065-78a861931196", null, "Admin", "ADMIN" },
                    { "9df2e771-5a34-456c-b55f-bb9d797f1830", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "470b2eb6-e03f-43c0-a065-78a861931196");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "9df2e771-5a34-456c-b55f-bb9d797f1830");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "AuxDatas");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f947ebb-298f-4fc3-8d42-90eca6f29531", null, "Admin", "ADMIN" },
                    { "8fbfb03f-bda4-45b7-8b9a-d8e798039af9", null, "Member", "MEMBER" }
                });
        }
    }
}
