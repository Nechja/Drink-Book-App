using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class icegarnish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Drinks_Name",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Garnish",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Ice",
                table: "Drinks");

            migrationBuilder.AddColumn<int>(
                name: "GarnishId",
                table: "Drinks",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IceId",
                table: "Drinks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GarnishDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarnishDataModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IceDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IceDataModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructionTags_Value",
                table: "InstructionTags",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_GarnishId",
                table: "Drinks",
                column: "GarnishId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_IceId",
                table: "Drinks",
                column: "IceId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_Name",
                table: "Drinks",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_GarnishDataModel_Value",
                table: "GarnishDataModel",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IceDataModel_Value",
                table: "IceDataModel",
                column: "Value",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_GarnishDataModel_GarnishId",
                table: "Drinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_IceDataModel_IceId",
                table: "Drinks");

            migrationBuilder.DropTable(
                name: "GarnishDataModel");

            migrationBuilder.DropTable(
                name: "IceDataModel");

            migrationBuilder.DropIndex(
                name: "IX_InstructionTags_Value",
                table: "InstructionTags");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_GarnishId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_IceId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_Name",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "GarnishId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "IceId",
                table: "Drinks");

            migrationBuilder.AddColumn<string>(
                name: "Garnish",
                table: "Drinks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ice",
                table: "Drinks",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_Name",
                table: "Drinks",
                column: "Name")
                .Annotation("Npgsql:IndexInclude", new[] { "Id" });
        }
    }
}
