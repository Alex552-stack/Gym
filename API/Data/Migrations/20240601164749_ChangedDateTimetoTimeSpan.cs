using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedDateTimetoTimeSpan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuxDatas",
                table: "AuxDatas");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "4f1098b3-11f4-49b7-a2f6-be6abc7b5ce2");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "65f1638e-3c17-4f2e-9cc2-5698c24c9f0b");

            migrationBuilder.RenameTable(
                name: "AuxDatas",
                newName: "AuxDates");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuxDates",
                table: "AuxDates",
                column: "Key");

            migrationBuilder.CreateTable(
                name: "Tiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TimeToCompleteRequirement = table.Column<TimeSpan>(type: "interval", nullable: false),
                    RequiredCount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b42684c3-713e-450b-85e9-ed075428a5ac", null, "Admin", "ADMIN" },
                    { "c1b8cf8b-642b-4dc8-9abc-87916abf5292", null, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "Tiers",
                columns: new[] { "Id", "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[,]
                {
                    { 1, "The basic tear, easy for newcomers to achieve", "Iron Tier", 10, new TimeSpan(30, 0, 0, 0, 0) },
                    { 2, "The first real milestone. You are starting to get stronger", "Bronze Tier", 30, new TimeSpan(60, 0, 0, 0, 0) },
                    { 3, "Now you are starting to impress people with your physique. Keep going", "Iron Tier", 40, new TimeSpan(90, 0, 0, 0, 0) },
                    { 4, "The gym is your second home. Or maybe even the firts...", "Gold Tier", 60, new TimeSpan(120, 0, 0, 0, 0) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuxDates",
                table: "AuxDates");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b42684c3-713e-450b-85e9-ed075428a5ac");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c1b8cf8b-642b-4dc8-9abc-87916abf5292");

            migrationBuilder.RenameTable(
                name: "AuxDates",
                newName: "AuxDatas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuxDatas",
                table: "AuxDatas",
                column: "Key");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f1098b3-11f4-49b7-a2f6-be6abc7b5ce2", null, "Member", "MEMBER" },
                    { "65f1638e-3c17-4f2e-9cc2-5698c24c9f0b", null, "Admin", "ADMIN" }
                });
        }
    }
}
