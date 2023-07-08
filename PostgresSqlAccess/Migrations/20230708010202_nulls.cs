using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class nulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Flags_Flagid",
                table: "Instructions");

            migrationBuilder.AlterColumn<int>(
                name: "Flagid",
                table: "Instructions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Flags_Flagid",
                table: "Instructions",
                column: "Flagid",
                principalTable: "Flags",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Flags_Flagid",
                table: "Instructions");

            migrationBuilder.AlterColumn<int>(
                name: "Flagid",
                table: "Instructions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Flags_Flagid",
                table: "Instructions",
                column: "Flagid",
                principalTable: "Flags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
