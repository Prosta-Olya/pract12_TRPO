using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pract12_TRPO.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Students",
                newName: "Login");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Students",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "Students",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Students",
                newName: "MiddleName");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Students",
                newName: "Birthday");
        }
    }
}
