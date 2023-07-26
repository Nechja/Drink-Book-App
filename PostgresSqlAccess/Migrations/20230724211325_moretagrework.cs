using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class moretagrework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_DrinkTags_DrinkTagDataModelId",
                table: "Drinks");

            migrationBuilder.DropTable(
                name: "DrinkTags");

            migrationBuilder.DropTable(
                name: "IngredientDataModelIngredientTagDataModel");

            migrationBuilder.DropTable(
                name: "InstructionDataModelInstructionTagDataModel");

            migrationBuilder.DropTable(
                name: "IngredientsTags");

            migrationBuilder.DropTable(
                name: "InstructionTags");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_DrinkTagDataModelId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "DrinkTagDataModelId",
                table: "Drinks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrinkTagDataModelId",
                table: "Drinks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrinkTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Mod = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstructionTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientDataModelIngredientTagDataModel",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientDataModelIngredientTagDataModel", x => new { x.IngredientsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_IngredientDataModelIngredientTagDataModel_IngredientsTags_T~",
                        column: x => x.TagsId,
                        principalTable: "IngredientsTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientDataModelIngredientTagDataModel_Ingredients_Ingre~",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructionDataModelInstructionTagDataModel",
                columns: table => new
                {
                    InstructionsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionDataModelInstructionTagDataModel", x => new { x.InstructionsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_InstructionDataModelInstructionTagDataModel_InstructionTags~",
                        column: x => x.TagsId,
                        principalTable: "InstructionTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstructionDataModelInstructionTagDataModel_Instructions_In~",
                        column: x => x.InstructionsId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_DrinkTagDataModelId",
                table: "Drinks",
                column: "DrinkTagDataModelId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDataModelIngredientTagDataModel_TagsId",
                table: "IngredientDataModelIngredientTagDataModel",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTags_Value",
                table: "IngredientsTags",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstructionDataModelInstructionTagDataModel_TagsId",
                table: "InstructionDataModelInstructionTagDataModel",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionTags_Value",
                table: "InstructionTags",
                column: "Value",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_DrinkTags_DrinkTagDataModelId",
                table: "Drinks",
                column: "DrinkTagDataModelId",
                principalTable: "DrinkTags",
                principalColumn: "Id");
        }
    }
}
