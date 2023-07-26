using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class StartingTagRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkDataModelDrinkTagDataModel");

            migrationBuilder.AddColumn<int>(
                name: "DrinkTagDataModelId",
                table: "Drinks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DrinkTagDataModelId",
                table: "Drinks",
                column: "DrinkTagDataModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_DrinkTags_DrinkTagDataModelId",
                table: "Drinks",
                column: "DrinkTagDataModelId",
                principalTable: "DrinkTags",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_DrinkTags_DrinkTagDataModelId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_DrinkTagDataModelId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "DrinkTagDataModelId",
                table: "Drinks");

            migrationBuilder.CreateTable(
                name: "DrinkDataModelDrinkTagDataModel",
                columns: table => new
                {
                    DrinksId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkDataModelDrinkTagDataModel", x => new { x.DrinksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_DrinkDataModelDrinkTagDataModel_DrinkTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "DrinkTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkDataModelDrinkTagDataModel_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelDrinkTagDataModel_TagsId",
                table: "DrinkDataModelDrinkTagDataModel",
                column: "TagsId");
        }
    }
}
