using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pract12_TRPO.Migrations
{
    /// <inheritdoc />
    public partial class AddInterestGroupRelationManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterestGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestGroups", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "UserInterestGroups",
                columns: table => new
                {
                    InterestGroupId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    JoineAt = table.Column<DateOnly>(type: "date", nullable: false),
                    IsModerator = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInterestGroups", x => new { x.StudentId, x.InterestGroupId });
                    table.ForeignKey(
                        name: "FK_UserInterestGroups_InterestGroups_InterestGroupId",
                        column: x => x.InterestGroupId,
                        principalTable: "InterestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInterestGroups_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestGroupStudent_StudentsId",
                table: "InterestGroupStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInterestGroups_InterestGroupId",
                table: "UserInterestGroups",
                column: "InterestGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestGroupStudent");

            migrationBuilder.DropTable(
                name: "UserInterestGroups");

            migrationBuilder.DropTable(
                name: "InterestGroups");
        }
    }
}
