using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Try2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "01e7d478-1d6e-4dbf-a264-776776c3082e");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5b89adf9-9c2b-45e7-872f-32b8de13d40c");

            migrationBuilder.CreateTable(
                name: "AuxDatas",
                columns: table => new
                {
                    Key = table.Column<string>(type: "character varying(55)", maxLength: 55, nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuxDatas", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5f947ebb-298f-4fc3-8d42-90eca6f29531", null, "Admin", "ADMIN" },
                    { "8fbfb03f-bda4-45b7-8b9a-d8e798039af9", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuxDatas");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5f947ebb-298f-4fc3-8d42-90eca6f29531");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "8fbfb03f-bda4-45b7-8b9a-d8e798039af9");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01e7d478-1d6e-4dbf-a264-776776c3082e", null, "Member", "MEMBER" },
                    { "5b89adf9-9c2b-45e7-872f-32b8de13d40c", null, "Admin", "ADMIN" }
                });
        }
    }
}
