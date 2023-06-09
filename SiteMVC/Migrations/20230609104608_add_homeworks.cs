using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteMVC.Migrations
{
    public partial class add_homeworks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Classes_ClassID",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Lessons_LessonId",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_LessonID",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "LessonID",
                table: "Journals");

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "HomeWorks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "HomeWorks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Classes_ClassID",
                table: "HomeWorks",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Lessons_LessonId",
                table: "HomeWorks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Classes_ClassID",
                table: "HomeWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeWorks_Lessons_LessonId",
                table: "HomeWorks");

            migrationBuilder.AddColumn<int>(
                name: "LessonID",
                table: "Journals",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LessonId",
                table: "HomeWorks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "HomeWorks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_LessonID",
                table: "Journals",
                column: "LessonID");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Classes_ClassID",
                table: "HomeWorks",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HomeWorks_Lessons_LessonId",
                table: "HomeWorks",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Lessons_LessonID",
                table: "Journals",
                column: "LessonID",
                principalTable: "Lessons",
                principalColumn: "Id");
        }
    }
}
