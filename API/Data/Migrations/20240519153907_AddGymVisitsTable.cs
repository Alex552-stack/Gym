using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGymVisitsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "421edef9-a8a2-492a-a3be-f9cbd23b9f1c");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a90bae2b-e575-4374-bdf5-d8c26fc5b26d");

            migrationBuilder.CreateTable(
                name: "GymVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Mounth = table.Column<int>(type: "integer", nullable: false),
                    Day = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymVisits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6bb7393b-af82-45b7-a869-375dfbb230b0", null, "Admin", "ADMIN" },
                    { "ae7d8bba-b1f1-4d6e-a1c9-f30c506a9329", null, "Member", "MEMBER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GymVisits_UserId",
                table: "GymVisits",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymVisits");

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
                    { "421edef9-a8a2-492a-a3be-f9cbd23b9f1c", null, "Member", "MEMBER" },
                    { "a90bae2b-e575-4374-bdf5-d8c26fc5b26d", null, "Admin", "ADMIN" }
                });
        }
    }
}
