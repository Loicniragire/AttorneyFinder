using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attorneys.Migrations
{
    /// <inheritdoc />
    public partial class removetokenfromdbmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpires",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "Attorneys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpires",
                table: "Attorneys",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TokenExpires",
                table: "Attorneys",
                type: "datetime2",
                nullable: true);
        }
    }
}
