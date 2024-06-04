using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b42684c3-713e-450b-85e9-ed075428a5ac");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "c1b8cf8b-642b-4dc8-9abc-87916abf5292");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "61c91630-971b-43f7-93e0-e771f024fbc7", null, "Member", "MEMBER" },
                    { "cd72480e-c991-4fcf-a46a-4423fd0c5439", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "The beggining of a new story", "Basic Tier", 0, new TimeSpan(1, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "The basic tear, easy for newcomers to achieve", "Iron Tier", 10, new TimeSpan(30, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "The first real milestone. You are starting to get stronger", "Bronze Tier", 30, new TimeSpan(60, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "Now you are starting to impress people with your physique. Keep going", "Iron Tier", 40, new TimeSpan(90, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "Tiers",
                columns: new[] { "Id", "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { 5, "The gym is your second home. Or maybe even the firts...", "Gold Tier", 60, new TimeSpan(120, 0, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "61c91630-971b-43f7-93e0-e771f024fbc7");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "cd72480e-c991-4fcf-a46a-4423fd0c5439");

            migrationBuilder.DeleteData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b42684c3-713e-450b-85e9-ed075428a5ac", null, "Admin", "ADMIN" },
                    { "c1b8cf8b-642b-4dc8-9abc-87916abf5292", null, "Member", "MEMBER" }
                });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "The basic tear, easy for newcomers to achieve", "Iron Tier", 10, new TimeSpan(30, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "The first real milestone. You are starting to get stronger", "Bronze Tier", 30, new TimeSpan(60, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "Now you are starting to impress people with your physique. Keep going", "Iron Tier", 40, new TimeSpan(90, 0, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "Tiers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Name", "RequiredCount", "TimeToCompleteRequirement" },
                values: new object[] { "The gym is your second home. Or maybe even the firts...", "Gold Tier", 60, new TimeSpan(120, 0, 0, 0, 0) });
        }
    }
}
