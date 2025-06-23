using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plan_It.Migrations
{
    /// <inheritdoc />
    public partial class PlanViewModelCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Plans",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Plans",
                newName: "IsCompleted");
        }
    }
}
