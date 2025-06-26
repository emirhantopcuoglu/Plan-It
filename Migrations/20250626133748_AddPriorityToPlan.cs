using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plan_It.Migrations
{
    /// <inheritdoc />
    public partial class AddPriorityToPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Plans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Plans");
        }
    }
}
