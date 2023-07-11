using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_GarnishTypes_GarnishId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "DropShopOptions",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "GarnishId",
                table: "Drinks",
                newName: "RimId");

            migrationBuilder.RenameIndex(
                name: "IX_Drinks_GarnishId",
                table: "Drinks",
                newName: "IX_Drinks_RimId");

            migrationBuilder.AddColumn<int>(
                name: "DisplayWeight",
                table: "Instructions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrinkDataModelGarnishDataModel",
                columns: table => new
                {
                    DrinksId = table.Column<int>(type: "integer", nullable: false),
                    GarnishesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkDataModelGarnishDataModel", x => new { x.DrinksId, x.GarnishesId });
                    table.ForeignKey(
                        name: "FK_DrinkDataModelGarnishDataModel_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkDataModelGarnishDataModel_GarnishTypes_GarnishesId",
                        column: x => x.GarnishesId,
                        principalTable: "GarnishTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RimDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RimDataModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelGarnishDataModel_GarnishesId",
                table: "DrinkDataModelGarnishDataModel",
                column: "GarnishesId");

            migrationBuilder.CreateIndex(
                name: "IX_RimDataModel_Value",
                table: "RimDataModel",
                column: "Value",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_RimDataModel_RimId",
                table: "Drinks",
                column: "RimId",
                principalTable: "RimDataModel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_RimDataModel_RimId",
                table: "Drinks");

            migrationBuilder.DropTable(
                name: "DrinkDataModelGarnishDataModel");

            migrationBuilder.DropTable(
                name: "RimDataModel");

            migrationBuilder.DropColumn(
                name: "DisplayWeight",
                table: "Instructions");

            migrationBuilder.RenameColumn(
                name: "RimId",
                table: "Drinks",
                newName: "GarnishId");

            migrationBuilder.RenameIndex(
                name: "IX_Drinks_RimId",
                table: "Drinks",
                newName: "IX_Drinks_GarnishId");

            migrationBuilder.AddColumn<string>(
                name: "DropShopOptions",
                table: "Instructions",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_GarnishTypes_GarnishId",
                table: "Drinks",
                column: "GarnishId",
                principalTable: "GarnishTypes",
                principalColumn: "Id");
        }
    }
}
