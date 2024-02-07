using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuddGokApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProgImpPropStudyProgram : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramImplementations_StudyPrograms_StudyProgramId",
                table: "ProgramImplementations");

            migrationBuilder.DropColumn(
                name: "ProgramId",
                table: "ProgramImplementations");

            migrationBuilder.AlterColumn<int>(
                name: "StudyProgramId",
                table: "ProgramImplementations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramImplementations_StudyPrograms_StudyProgramId",
                table: "ProgramImplementations",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramImplementations_StudyPrograms_StudyProgramId",
                table: "ProgramImplementations");

            migrationBuilder.AlterColumn<int>(
                name: "StudyProgramId",
                table: "ProgramImplementations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProgramId",
                table: "ProgramImplementations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramImplementations_StudyPrograms_StudyProgramId",
                table: "ProgramImplementations",
                column: "StudyProgramId",
                principalTable: "StudyPrograms",
                principalColumn: "Id");
        }
    }
}
