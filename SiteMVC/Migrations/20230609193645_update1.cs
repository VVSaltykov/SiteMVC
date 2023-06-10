using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteMVC.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Classes_ClassID",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Classes_ClassID",
                table: "Lessons",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Classes_ClassID",
                table: "Lessons");

            migrationBuilder.AlterColumn<int>(
                name: "ClassID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Classes_ClassID",
                table: "Lessons",
                column: "ClassID",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
