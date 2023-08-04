using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class drinklistfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDataModelUserDrinkListsDataModel");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DrinkLists",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkLists_UserId",
                table: "DrinkLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkLists_Users_UserId",
                table: "DrinkLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkLists_Users_UserId",
                table: "DrinkLists");

            migrationBuilder.DropIndex(
                name: "IX_DrinkLists_UserId",
                table: "DrinkLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DrinkLists");

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
                name: "IX_UserDataModelUserDrinkListsDataModel_UsersId",
                table: "UserDataModelUserDrinkListsDataModel",
                column: "UsersId");
        }
    }
}
