using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuddGokApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExamGroupsAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamGroup_Exams_ExamId",
                table: "ExamGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamGroup_Users_UserId",
                table: "ExamGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamGroup",
                table: "ExamGroup");

            migrationBuilder.RenameTable(
                name: "ExamGroup",
                newName: "ExamGroups");

            migrationBuilder.RenameIndex(
                name: "IX_ExamGroup_UserId",
                table: "ExamGroups",
                newName: "IX_ExamGroups_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamGroup_ExamId",
                table: "ExamGroups",
                newName: "IX_ExamGroups_ExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamGroups",
                table: "ExamGroups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamGroups_Exams_ExamId",
                table: "ExamGroups",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamGroups_Users_UserId",
                table: "ExamGroups",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamGroups_Exams_ExamId",
                table: "ExamGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamGroups_Users_UserId",
                table: "ExamGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamGroups",
                table: "ExamGroups");

            migrationBuilder.RenameTable(
                name: "ExamGroups",
                newName: "ExamGroup");

            migrationBuilder.RenameIndex(
                name: "IX_ExamGroups_UserId",
                table: "ExamGroup",
                newName: "IX_ExamGroup_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamGroups_ExamId",
                table: "ExamGroup",
                newName: "IX_ExamGroup_ExamId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamGroup",
                table: "ExamGroup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamGroup_Exams_ExamId",
                table: "ExamGroup",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamGroup_Users_UserId",
                table: "ExamGroup",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
