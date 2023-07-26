using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class tagfixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TagTypeId",
                table: "DrinkTags",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DrinkTagTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkTagTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkTags_TagTypeId",
                table: "DrinkTags",
                column: "TagTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkTags_DrinkTagTypes_TagTypeId",
                table: "DrinkTags",
                column: "TagTypeId",
                principalTable: "DrinkTagTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkTags_DrinkTagTypes_TagTypeId",
                table: "DrinkTags");

            migrationBuilder.DropTable(
                name: "DrinkTagTypes");

            migrationBuilder.DropIndex(
                name: "IX_DrinkTags_TagTypeId",
                table: "DrinkTags");

            migrationBuilder.DropColumn(
                name: "TagTypeId",
                table: "DrinkTags");
        }
    }
}
