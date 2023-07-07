using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_ServingFlags_ServingFlagid",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_ShakerFlags_ShakerFlagid",
                table: "Instructions");

            migrationBuilder.DropTable(
                name: "ServingFlags");

            migrationBuilder.DropTable(
                name: "ShakerFlags");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_ServingFlagid",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "ServingFlagid",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "ShakerFlagid",
                table: "Instructions",
                newName: "Flagid");

            migrationBuilder.RenameIndex(
                name: "IX_Instructions_ShakerFlagid",
                table: "Instructions",
                newName: "IX_Instructions_Flagid");

            migrationBuilder.AddColumn<string>(
                name: "DropShopOptions",
                table: "Instructions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mod",
                table: "IngredientsTags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OpeningStatement = table.Column<string>(type: "text", nullable: true),
                    ClosingStatment = table.Column<string>(type: "text", nullable: true),
                    InlineStatement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flags_Name",
                table: "Flags",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Flags_Flagid",
                table: "Instructions",
                column: "Flagid",
                principalTable: "Flags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Flags_Flagid",
                table: "Instructions");

            migrationBuilder.DropTable(
                name: "Flags");

            migrationBuilder.DropColumn(
                name: "DropShopOptions",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "Mod",
                table: "IngredientsTags");

            migrationBuilder.RenameColumn(
                name: "Flagid",
                table: "Instructions",
                newName: "ShakerFlagid");

            migrationBuilder.RenameIndex(
                name: "IX_Instructions_Flagid",
                table: "Instructions",
                newName: "IX_Instructions_ShakerFlagid");

            migrationBuilder.AddColumn<int>(
                name: "ServingFlagid",
                table: "Instructions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServingFlags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClosingStatment = table.Column<string>(type: "text", nullable: true),
                    InlineStatement = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OpeningStatement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServingFlags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ShakerFlags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClosingStatment = table.Column<string>(type: "text", nullable: true),
                    InlineStatement = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OpeningStatement = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShakerFlags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_ServingFlagid",
                table: "Instructions",
                column: "ServingFlagid");

            migrationBuilder.CreateIndex(
                name: "IX_ServingFlags_Name",
                table: "ServingFlags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShakerFlags_Name",
                table: "ShakerFlags",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_ServingFlags_ServingFlagid",
                table: "Instructions",
                column: "ServingFlagid",
                principalTable: "ServingFlags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_ShakerFlags_ShakerFlagid",
                table: "Instructions",
                column: "ShakerFlagid",
                principalTable: "ShakerFlags",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
