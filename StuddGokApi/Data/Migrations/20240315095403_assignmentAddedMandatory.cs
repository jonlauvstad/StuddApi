using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuddGokApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class assignmentAddedMandatory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Mandatory",
                table: "Assignments",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mandatory",
                table: "Assignments");
        }
    }
}
