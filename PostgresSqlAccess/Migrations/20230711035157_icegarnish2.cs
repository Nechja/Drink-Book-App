using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class icegarnish2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_GarnishDataModel_GarnishId",
                table: "Drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_IceDataModel_IceId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IceDataModel",
                table: "IceDataModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarnishDataModel",
                table: "GarnishDataModel");

            migrationBuilder.RenameTable(
                name: "IceDataModel",
                newName: "IceTypes");

            migrationBuilder.RenameTable(
                name: "GarnishDataModel",
                newName: "GarnishTypes");

            migrationBuilder.RenameIndex(
                name: "IX_IceDataModel_Value",
                table: "IceTypes",
                newName: "IX_IceTypes_Value");

            migrationBuilder.RenameIndex(
                name: "IX_GarnishDataModel_Value",
                table: "GarnishTypes",
                newName: "IX_GarnishTypes_Value");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IceTypes",
                table: "IceTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarnishTypes",
                table: "GarnishTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_GarnishTypes_GarnishId",
                table: "Drinks",
                column: "GarnishId",
                principalTable: "GarnishTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_IceTypes_IceId",
                table: "Drinks",
                column: "IceId",
                principalTable: "IceTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_GarnishTypes_GarnishId",
                table: "Drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_IceTypes_IceId",
                table: "Drinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IceTypes",
                table: "IceTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarnishTypes",
                table: "GarnishTypes");

            migrationBuilder.RenameTable(
                name: "IceTypes",
                newName: "IceDataModel");

            migrationBuilder.RenameTable(
                name: "GarnishTypes",
                newName: "GarnishDataModel");

            migrationBuilder.RenameIndex(
                name: "IX_IceTypes_Value",
                table: "IceDataModel",
                newName: "IX_IceDataModel_Value");

            migrationBuilder.RenameIndex(
                name: "IX_GarnishTypes_Value",
                table: "GarnishDataModel",
                newName: "IX_GarnishDataModel_Value");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IceDataModel",
                table: "IceDataModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarnishDataModel",
                table: "GarnishDataModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_GarnishDataModel_GarnishId",
                table: "Drinks",
                column: "GarnishId",
                principalTable: "GarnishDataModel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_IceDataModel_IceId",
                table: "Drinks",
                column: "IceId",
                principalTable: "IceDataModel",
                principalColumn: "Id");
        }
    }
}
