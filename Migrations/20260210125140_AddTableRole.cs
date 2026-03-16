using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pract12_TRPO.Migrations
{
    /// <inheritdoc />
    public partial class AddTableRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.InsertData(
             table: "Roles",
             columns: new[] { "Id", "Title" },
             values: new object[] { 1, "Пользователь" }
             );

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoleId",
                table: "Students",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Roles_RoleId",
                table: "Students",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Roles_RoleId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Students_RoleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Students");
        }
    }
}
