using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FirstRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FontAwesomeIcon",
                table: "Glasses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Verification",
                table: "Drinks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "LinkTypeDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTypeDataModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Clicks = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkDataModel_LinkTypeDataModel_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LinkTypeDataModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientDataModelLinkDataModel",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "integer", nullable: false),
                    LinksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientDataModelLinkDataModel", x => new { x.IngredientsId, x.LinksId });
                    table.ForeignKey(
                        name: "FK_IngredientDataModelLinkDataModel_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientDataModelLinkDataModel_LinkDataModel_LinksId",
                        column: x => x.LinksId,
                        principalTable: "LinkDataModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDataModelLinkDataModel_LinksId",
                table: "IngredientDataModelLinkDataModel",
                column: "LinksId");

            migrationBuilder.CreateIndex(
                name: "IX_LinkDataModel_TypeId",
                table: "LinkDataModel",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientDataModelLinkDataModel");

            migrationBuilder.DropTable(
                name: "LinkDataModel");

            migrationBuilder.DropTable(
                name: "LinkTypeDataModel");

            migrationBuilder.DropColumn(
                name: "FontAwesomeIcon",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "Verification",
                table: "Drinks");
        }
    }
}
