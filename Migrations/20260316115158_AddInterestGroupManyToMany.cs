using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pract12_TRPO.Migrations
{
    /// <inheritdoc />
    public partial class AddInterestGroupManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterestGroups_Students_StudentId",
                table: "UserInterestGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Students_StudentId",
                table: "UserProfile");

            migrationBuilder.DropTable(
                name: "InterestGroupStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile");

            migrationBuilder.RenameTable(
                name: "UserProfile",
                newName: "UserProfiles");

            migrationBuilder.RenameColumn(
                name: "JoineAt",
                table: "UserInterestGroups",
                newName: "JoinedAt");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "UserInterestGroups",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfile_StudentId",
                table: "UserProfiles",
                newName: "IX_UserProfiles_StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "InterestGroups",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InterestGroups_Title",
                table: "InterestGroups",
                column: "Title",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterestGroups_Students_UserId",
                table: "UserInterestGroups",
                column: "UserId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_Students_StudentId",
                table: "UserProfiles",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInterestGroups_Students_UserId",
                table: "UserInterestGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_Students_StudentId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_InterestGroups_Title",
                table: "InterestGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "JoinedAt",
                table: "UserInterestGroups",
                newName: "JoineAt");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserInterestGroups",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProfiles_StudentId",
                table: "UserProfile",
                newName: "IX_UserProfile_StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "InterestGroups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InterestGroupStudent",
                columns: table => new
                {
                    InterestGroupsId = table.Column<int>(type: "int", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestGroupStudent", x => new { x.InterestGroupsId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_InterestGroupStudent_InterestGroups_InterestGroupsId",
                        column: x => x.InterestGroupsId,
                        principalTable: "InterestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestGroupStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestGroupStudent_StudentsId",
                table: "InterestGroupStudent",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInterestGroups_Students_StudentId",
                table: "UserInterestGroups",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Students_StudentId",
                table: "UserProfile",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
