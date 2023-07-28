using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrinkLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    DarkMode = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinkDataModelUserDrinkListsDataModel",
                columns: table => new
                {
                    DrinkListsId = table.Column<int>(type: "integer", nullable: false),
                    DrinksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkDataModelUserDrinkListsDataModel", x => new { x.DrinkListsId, x.DrinksId });
                    table.ForeignKey(
                        name: "FK_DrinkDataModelUserDrinkListsDataModel_DrinkLists_DrinkLists~",
                        column: x => x.DrinkListsId,
                        principalTable: "DrinkLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinkDataModelUserDrinkListsDataModel_Drinks_DrinksId",
                        column: x => x.DrinksId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDataModelUserDrinkListsDataModel",
                columns: table => new
                {
                    DrinkListsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDataModelUserDrinkListsDataModel", x => new { x.DrinkListsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserDataModelUserDrinkListsDataModel_DrinkLists_DrinkListsId",
                        column: x => x.DrinkListsId,
                        principalTable: "DrinkLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDataModelUserDrinkListsDataModel_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelUserDrinkListsDataModel_DrinksId",
                table: "DrinkDataModelUserDrinkListsDataModel",
                column: "DrinksId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDataModelUserDrinkListsDataModel_UsersId",
                table: "UserDataModelUserDrinkListsDataModel",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkDataModelUserDrinkListsDataModel");

            migrationBuilder.DropTable(
                name: "UserDataModelUserDrinkListsDataModel");

            migrationBuilder.DropTable(
                name: "DrinkLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
