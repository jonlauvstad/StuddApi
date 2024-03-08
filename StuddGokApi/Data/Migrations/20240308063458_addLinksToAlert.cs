using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuddGokApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addLinksToAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Links",
                table: "Alerts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Links",
                table: "Alerts");
        }
    }
}
