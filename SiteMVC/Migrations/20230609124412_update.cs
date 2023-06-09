using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteMVC.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals");

            migrationBuilder.AlterColumn<int>(
                name: "LessonID",
                table: "Journals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals");

            migrationBuilder.AlterColumn<int>(
                name: "LessonID",
                table: "Journals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
