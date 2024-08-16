using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attorneys.Migrations
{
    /// <inheritdoc />
    public partial class updatingtablecolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Roles",
                table: "Attorneys",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Attorneys",
                newName: "Roles");
        }
    }
}
