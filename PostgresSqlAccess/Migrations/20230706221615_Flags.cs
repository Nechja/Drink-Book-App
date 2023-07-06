using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Flags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClosingStatment",
                table: "ShakerFlags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InlineStatement",
                table: "ShakerFlags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpeningStatement",
                table: "ShakerFlags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClosingStatment",
                table: "ServingFlags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InlineStatement",
                table: "ServingFlags",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpeningStatement",
                table: "ServingFlags",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClosingStatment",
                table: "ShakerFlags");

            migrationBuilder.DropColumn(
                name: "InlineStatement",
                table: "ShakerFlags");

            migrationBuilder.DropColumn(
                name: "OpeningStatement",
                table: "ShakerFlags");

            migrationBuilder.DropColumn(
                name: "ClosingStatment",
                table: "ServingFlags");

            migrationBuilder.DropColumn(
                name: "InlineStatement",
                table: "ServingFlags");

            migrationBuilder.DropColumn(
                name: "OpeningStatement",
                table: "ServingFlags");
        }
    }
}
