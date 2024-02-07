using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StuddGokApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class VenueCapacity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "Venues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Venues");
        }
    }
}
