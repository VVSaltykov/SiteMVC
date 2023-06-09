using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteMVC.Migrations
{
    public partial class add_journals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonID",
                table: "Journals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_LessonID",
                table: "Journals",
                column: "LessonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_LessonID",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "LessonID",
                table: "Journals");
        }
    }
}
