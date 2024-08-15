using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attorneys.Migrations
{
    /// <inheritdoc />
    public partial class attorneys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Attorneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Attorneys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                name: "Roles",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Attorneys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Attorneys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpires",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "TokenExpires",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Attorneys");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Attorneys");
        }
    }
}
