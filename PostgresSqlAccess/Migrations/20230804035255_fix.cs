using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "DrinkTagTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkTagTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flags",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    OpeningStatement = table.Column<string>(type: "text", nullable: true),
                    ClosingStatment = table.Column<string>(type: "text", nullable: true),
                    InlineStatement = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GarnishTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarnishTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Glasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Oz = table.Column<float>(type: "real", nullable: true),
                    FontAwesomeIcon = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Mod = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientTypes",
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
                    table.PrimaryKey("PK_IngredientTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstructionTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkTypeDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Info = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTypeDataModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RimTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<byte[]>(type: "bytea", nullable: false),
                    UserDisplayName = table.Column<string>(type: "text", nullable: false),
                    DarkMode = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinkTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    TagTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinkTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrinkTags_DrinkTagTypes_TagTypeId",
                        column: x => x.TagTypeId,
                        principalTable: "DrinkTagTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IngredientTypeId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientTypes_IngredientTypeId",
                        column: x => x.IngredientTypeId,
                        principalTable: "IngredientTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LinkDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Link = table.Column<string>(type: "text", nullable: false),
                    Clicks = table.Column<int>(type: "integer", nullable: false),
                    TypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LinkDataModel_LinkTypeDataModel_TypeId",
                        column: x => x.TypeId,
                        principalTable: "LinkTypeDataModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Verification = table.Column<bool>(type: "boolean", nullable: false),
                    IceId = table.Column<int>(type: "integer", nullable: true),
                    RimId = table.Column<int>(type: "integer", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    ModId = table.Column<int>(type: "integer", nullable: true),
                    GlassId = table.Column<int>(type: "integer", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Link = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FINALDELETE = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drinks_Drinks_ModId",
                        column: x => x.ModId,
                        principalTable: "Drinks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drinks_Glasses_GlassId",
                        column: x => x.GlassId,
                        principalTable: "Glasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drinks_IceTypes_IceId",
                        column: x => x.IceId,
                        principalTable: "IceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Drinks_RimTypes_RimId",
                        column: x => x.RimId,
                        principalTable: "RimTypes",
                        principalColumn: "Id");
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
                name: "IngredientDataModelLinkDataModel",
                columns: table => new
                {
                    IngredientsId = table.Column<int>(type: "integer", nullable: false),
                    LinksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientDataModelLinkDataModel", x => new { x.IngredientsId, x.LinksId });
                    table.ForeignKey(
                        name: "FK_IngredientDataModelLinkDataModel_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientDataModelLinkDataModel_LinkDataModel_LinksId",
                        column: x => x.LinksId,
                        principalTable: "LinkDataModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Oz = table.Column<float>(type: "real", nullable: true),
                    Special = table.Column<string>(type: "text", nullable: true),
                    DisplayWeight = table.Column<int>(type: "integer", nullable: true),
                    Flagid = table.Column<int>(type: "integer", nullable: true),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    DrinkId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instructions_Flags_Flagid",
                        column: x => x.Flagid,
                        principalTable: "Flags",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Instructions_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewsDataModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Views = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewsDataModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewsDataModel_Drinks_Views",
                        column: x => x.Views,
                        principalTable: "Drinks",
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
                name: "IX_DrinkBadgeDataModelDrinkDataModel_DrinksId",
                table: "DrinkBadgeDataModelDrinkDataModel",
                column: "DrinksId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelDrinkTagDataModel_TagsId",
                table: "DrinkDataModelDrinkTagDataModel",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelGarnishDataModel_GarnishesId",
                table: "DrinkDataModelGarnishDataModel",
                column: "GarnishesId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkDataModelUserDrinkListsDataModel_DrinksId",
                table: "DrinkDataModelUserDrinkListsDataModel",
                column: "DrinksId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_GlassId",
                table: "Drinks",
                column: "GlassId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_IceId",
                table: "Drinks",
                column: "IceId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_ModId",
                table: "Drinks",
                column: "ModId");

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_Name",
                table: "Drinks",
                column: "Name",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_RimId",
                table: "Drinks",
                column: "RimId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinkTags_TagTypeId",
                table: "DrinkTags",
                column: "TagTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flags_Name",
                table: "Flags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GarnishTypes_Value",
                table: "GarnishTypes",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IceTypes_Value",
                table: "IceTypes",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDataModelIngredientTagDataModel_TagsId",
                table: "IngredientDataModelIngredientTagDataModel",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientDataModelLinkDataModel_LinksId",
                table: "IngredientDataModelLinkDataModel",
                column: "LinksId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientTypeId",
                table: "Ingredients",
                column: "IngredientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsTags_Value",
                table: "IngredientsTags",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IngredientTypes_Name",
                table: "IngredientTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstructionDataModelInstructionTagDataModel_TagsId",
                table: "InstructionDataModelInstructionTagDataModel",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_DrinkId",
                table: "Instructions",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_Flagid",
                table: "Instructions",
                column: "Flagid");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_IngredientId",
                table: "Instructions",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionTags_Value",
                table: "InstructionTags",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LinkDataModel_TypeId",
                table: "LinkDataModel",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RimTypes_Value",
                table: "RimTypes",
                column: "Value",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDataModelUserDrinkListsDataModel_UsersId",
                table: "UserDataModelUserDrinkListsDataModel",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewsDataModel_Views",
                table: "ViewsDataModel",
                column: "Views",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinkBadgeDataModelDrinkDataModel");

            migrationBuilder.DropTable(
                name: "DrinkDataModelDrinkTagDataModel");

            migrationBuilder.DropTable(
                name: "DrinkDataModelGarnishDataModel");

            migrationBuilder.DropTable(
                name: "DrinkDataModelUserDrinkListsDataModel");

            migrationBuilder.DropTable(
                name: "IngredientDataModelIngredientTagDataModel");

            migrationBuilder.DropTable(
                name: "IngredientDataModelLinkDataModel");

            migrationBuilder.DropTable(
                name: "InstructionDataModelInstructionTagDataModel");

            migrationBuilder.DropTable(
                name: "UserDataModelUserDrinkListsDataModel");

            migrationBuilder.DropTable(
                name: "ViewsDataModel");

            migrationBuilder.DropTable(
                name: "DrinkBadges");

            migrationBuilder.DropTable(
                name: "DrinkTags");

            migrationBuilder.DropTable(
                name: "GarnishTypes");

            migrationBuilder.DropTable(
                name: "IngredientsTags");

            migrationBuilder.DropTable(
                name: "LinkDataModel");

            migrationBuilder.DropTable(
                name: "InstructionTags");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "DrinkLists");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DrinkTagTypes");

            migrationBuilder.DropTable(
                name: "LinkTypeDataModel");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Flags");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Glasses");

            migrationBuilder.DropTable(
                name: "IceTypes");

            migrationBuilder.DropTable(
                name: "RimTypes");

            migrationBuilder.DropTable(
                name: "IngredientTypes");
        }
    }
}
