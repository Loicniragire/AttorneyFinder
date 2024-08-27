using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Attorneys.Migrations
{
    /// <inheritdoc />
    public partial class seeddataadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LawFirm",
                table: "Attorneys",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Attorneys",
                columns: new[] { "Id", "CreatedAt", "Email", "IsDeleted", "LawFirm", "Name", "Password", "Phone", "PracticeArea", "Role", "UpdatedAt", "Username" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 8, 27, 9, 51, 20, 15, DateTimeKind.Unspecified).AddTicks(8860), new TimeSpan(0, -4, 0, 0, 0)), "john.doe@example.com", false, "Law Firm 1", "John Doe", "password", "123-456-7890", "Practice Area 1", "Admin", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "john.doe" },
                    { 2, new DateTimeOffset(new DateTime(2024, 8, 27, 9, 51, 20, 15, DateTimeKind.Unspecified).AddTicks(8930), new TimeSpan(0, -4, 0, 0, 0)), "jane.smith@example.com", false, "Law Firm 1", "Jane Smith", "password", "123-456-7890", "Practice Area 1", "User", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "jane.smith" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attorneys_LawFirm",
                table: "Attorneys",
                column: "LawFirm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attorneys_LawFirm",
                table: "Attorneys");

            migrationBuilder.DeleteData(
                table: "Attorneys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Attorneys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "LawFirm",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
