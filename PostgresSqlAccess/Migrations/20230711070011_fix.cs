using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_RimDataModel_RimId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RimDataModel",
                table: "RimDataModel");

            migrationBuilder.RenameTable(
                name: "RimDataModel",
                newName: "RimTypes");

            migrationBuilder.RenameIndex(
                name: "IX_RimDataModel_Value",
                table: "RimTypes",
                newName: "IX_RimTypes_Value");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RimTypes",
                table: "RimTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_RimTypes_RimId",
                table: "Drinks",
                column: "RimId",
                principalTable: "RimTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_RimTypes_RimId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RimTypes",
                table: "RimTypes");

            migrationBuilder.RenameTable(
                name: "RimTypes",
                newName: "RimDataModel");

            migrationBuilder.RenameIndex(
                name: "IX_RimTypes_Value",
                table: "RimDataModel",
                newName: "IX_RimDataModel_Value");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RimDataModel",
                table: "RimDataModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_RimDataModel_RimId",
                table: "Drinks",
                column: "RimId",
                principalTable: "RimDataModel",
                principalColumn: "Id");
        }
    }
}
