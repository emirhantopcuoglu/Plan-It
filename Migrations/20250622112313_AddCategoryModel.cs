using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plan_It.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Plans",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plans_CategoryId",
                table: "Plans",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plans_Categories_CategoryId",
                table: "Plans",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plans_Categories_CategoryId",
                table: "Plans");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Plans_CategoryId",
                table: "Plans");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Plans");
        }
    }
}
