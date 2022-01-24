using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charterio.Data.Migrations
{
    public partial class UserQuestionsRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserQuestions_AspNetUsers_UserId",
                table: "UserQuestions");

            migrationBuilder.DropIndex(
                name: "IX_UserQuestions_UserId",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserQuestions");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "UserQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserPhone",
                table: "UserQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserQuestions");

            migrationBuilder.DropColumn(
                name: "UserPhone",
                table: "UserQuestions");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserQuestions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_UserId",
                table: "UserQuestions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserQuestions_AspNetUsers_UserId",
                table: "UserQuestions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
