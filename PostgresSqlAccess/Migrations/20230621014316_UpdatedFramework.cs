using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFramework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModId",
                table: "Drinks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_ModId",
                table: "Drinks",
                column: "ModId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_Drinks_ModId",
                table: "Drinks",
                column: "ModId",
                principalTable: "Drinks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_Drinks_ModId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_ModId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "ModId",
                table: "Drinks");
        }
    }
}
