using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedindexfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkDataModelDrinkTagsDataModel");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DrinkTags",
                newName: "Id");

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
                name: "IX_Drinks_Name",
                table: "Drinks",
                column: "Name")
                .Annotation("Npgsql:IndexInclude", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelDrinkTagDataModel_TagsId",
                table: "DrinkDataModelDrinkTagDataModel",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkDataModelDrinkTagDataModel");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_Name",
                table: "Drinks");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DrinkTags",
                newName: "id");

            migrationBuilder.CreateTable(
                name: "DrinkDataModelDrinkTagsDataModel",
                columns: table => new
                {
                    DrinksId = table.Column<int>(type: "integer", nullable: false),
                    Tagsid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkDataModelDrinkTagsDataModel", x => new { x.DrinksId, x.Tagsid });
                    table.ForeignKey(
                        name: "FK_DrinkDataModelDrinkTagsDataModel_DrinkTags_Tagsid",
                        column: x => x.Tagsid,
                        principalTable: "DrinkTags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkDataModelDrinkTagsDataModel_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelDrinkTagsDataModel_Tagsid",
                table: "DrinkDataModelDrinkTagsDataModel",
                column: "Tagsid");
        }
    }
}
