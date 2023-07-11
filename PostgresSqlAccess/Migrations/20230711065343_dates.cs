using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class dates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "RimDataModel",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "InstructionTags",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Instructions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "IngredientTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "IngredientsTags",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Ingredients",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "IceTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Glasses",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "GarnishTypes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Flags",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "DrinkTags",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Drinks",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "RimDataModel");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "InstructionTags");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "IngredientTypes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "IngredientsTags");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "IceTypes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Glasses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "GarnishTypes");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Flags");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "DrinkTags");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Drinks");
        }
    }
}
