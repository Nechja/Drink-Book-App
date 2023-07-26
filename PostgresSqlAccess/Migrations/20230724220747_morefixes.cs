using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class morefixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "LinkTypeDataModel",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "LinkTypeDataModel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "DrinkTagTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Drinks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DrinkBadges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    HoverText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkBadges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinkBadgeDataModelDrinkDataModel",
                columns: table => new
                {
                    BadgesId = table.Column<int>(type: "integer", nullable: false),
                    DrinksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkBadgeDataModelDrinkDataModel", x => new { x.BadgesId, x.DrinksId });
                    table.ForeignKey(
                        name: "FK_DrinkBadgeDataModelDrinkDataModel_DrinkBadges_BadgesId",
                        column: x => x.BadgesId,
                        principalTable: "DrinkBadges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkBadgeDataModelDrinkDataModel_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkBadgeDataModelDrinkDataModel_DrinksId",
                table: "DrinkBadgeDataModelDrinkDataModel",
                column: "DrinksId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkBadgeDataModelDrinkDataModel");

            migrationBuilder.DropTable(
                name: "DrinkBadges");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "LinkTypeDataModel");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "DrinkTagTypes");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Drinks");

            migrationBuilder.AlterColumn<string>(
                name: "Info",
                table: "LinkTypeDataModel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
