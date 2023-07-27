﻿// <auto-generated />
using System;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DrinkDBContext))]
    [Migration("20230727010147_fixnull")]
    partial class fixnull
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.DrinkBadgeDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("HoverText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DrinkBadges");
                });

            modelBuilder.Entity("DataAccess.Models.DrinkDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool?>("FINALDELETE")
                        .HasColumnType("boolean");

                    b.Property<int?>("GlassId")
                        .HasColumnType("integer");

                    b.Property<int?>("IceId")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Link")
                        .HasColumnType("text");

                    b.Property<int?>("ModId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.Property<int?>("RimId")
                        .HasColumnType("integer");

                    b.Property<bool>("Verification")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("GlassId");

                    b.HasIndex("IceId");

                    b.HasIndex("ModId");

                    b.HasIndex("Name")
                        .IsUnique();

                    NpgsqlIndexBuilderExtensions.IncludeProperties(b.HasIndex("Name"), new[] { "Id" });

                    b.HasIndex("RimId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("DataAccess.Models.DrinkTagDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TagTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TagTypeId");

                    b.ToTable("DrinkTags");
                });

            modelBuilder.Entity("DataAccess.Models.DrinkTagType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DrinkTagTypes");
                });

            modelBuilder.Entity("DataAccess.Models.FlagDataModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("ClosingStatment")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InlineStatement")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("OpeningStatement")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("DataAccess.Models.GarnishDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("GarnishTypes");
                });

            modelBuilder.Entity("DataAccess.Models.GlassDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FontAwesomeIcon")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<float?>("Oz")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Glasses");
                });

            modelBuilder.Entity("DataAccess.Models.IceDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("IceTypes");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IngredientTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IngredientTypeId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientTagDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Mod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("IngredientsTags");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientTypeDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("IngredientTypes");
                });

            modelBuilder.Entity("DataAccess.Models.InstructionDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("DisplayWeight")
                        .HasColumnType("integer");

                    b.Property<int>("DrinkId")
                        .HasColumnType("integer");

                    b.Property<int?>("Flagid")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float?>("Oz")
                        .HasColumnType("real");

                    b.Property<string>("Special")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.HasIndex("Flagid");

                    b.HasIndex("IngredientId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("DataAccess.Models.InstructionTagDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("InstructionTags");
                });

            modelBuilder.Entity("DataAccess.Models.LinkDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Clicks")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("LinkDataModel");
                });

            modelBuilder.Entity("DataAccess.Models.LinkTypeDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Info")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LinkTypeDataModel");
                });

            modelBuilder.Entity("DataAccess.Models.RimDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("RimTypes");
                });

            modelBuilder.Entity("DataAccess.Models.ViewsDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Views")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Views")
                        .IsUnique();

                    b.ToTable("ViewsDataModel");
                });

            modelBuilder.Entity("DrinkBadgeDataModelDrinkDataModel", b =>
                {
                    b.Property<int>("BadgesId")
                        .HasColumnType("integer");

                    b.Property<int>("DrinksId")
                        .HasColumnType("integer");

                    b.HasKey("BadgesId", "DrinksId");

                    b.HasIndex("DrinksId");

                    b.ToTable("DrinkBadgeDataModelDrinkDataModel");
                });

            modelBuilder.Entity("DrinkDataModelDrinkTagDataModel", b =>
                {
                    b.Property<int>("DrinksId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("DrinksId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("DrinkDataModelDrinkTagDataModel");
                });

            modelBuilder.Entity("DrinkDataModelGarnishDataModel", b =>
                {
                    b.Property<int>("DrinksId")
                        .HasColumnType("integer");

                    b.Property<int>("GarnishesId")
                        .HasColumnType("integer");

                    b.HasKey("DrinksId", "GarnishesId");

                    b.HasIndex("GarnishesId");

                    b.ToTable("DrinkDataModelGarnishDataModel");
                });

            modelBuilder.Entity("IngredientDataModelIngredientTagDataModel", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("IngredientsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("IngredientDataModelIngredientTagDataModel");
                });

            modelBuilder.Entity("IngredientDataModelLinkDataModel", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("integer");

                    b.Property<int>("LinksId")
                        .HasColumnType("integer");

                    b.HasKey("IngredientsId", "LinksId");

                    b.HasIndex("LinksId");

                    b.ToTable("IngredientDataModelLinkDataModel");
                });

            modelBuilder.Entity("InstructionDataModelInstructionTagDataModel", b =>
                {
                    b.Property<int>("InstructionsId")
                        .HasColumnType("integer");

                    b.Property<int>("TagsId")
                        .HasColumnType("integer");

                    b.HasKey("InstructionsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("InstructionDataModelInstructionTagDataModel");
                });

            modelBuilder.Entity("DataAccess.Models.DrinkDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.GlassDataModel", "Glass")
                        .WithMany("Drinks")
                        .HasForeignKey("GlassId");

                    b.HasOne("DataAccess.Models.IceDataModel", "Ice")
                        .WithMany("Drinks")
                        .HasForeignKey("IceId");

                    b.HasOne("DataAccess.Models.DrinkDataModel", "Mod")
                        .WithMany()
                        .HasForeignKey("ModId");

                    b.HasOne("DataAccess.Models.RimDataModel", "Rim")
                        .WithMany("Drinks")
                        .HasForeignKey("RimId");

                    b.Navigation("Glass");

                    b.Navigation("Ice");

                    b.Navigation("Mod");

                    b.Navigation("Rim");
                });

            modelBuilder.Entity("DataAccess.Models.DrinkTagDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkTagType", "TagType")
                        .WithMany("DrinkTags")
                        .HasForeignKey("TagTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TagType");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.IngredientTypeDataModel", "IngredientType")
                        .WithMany("Ingredients")
                        .HasForeignKey("IngredientTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IngredientType");
                });

            modelBuilder.Entity("DataAccess.Models.InstructionDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkDataModel", "Drink")
                        .WithMany("Instructions")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.FlagDataModel", "Flag")
                        .WithMany("Instructions")
                        .HasForeignKey("Flagid");

                    b.HasOne("DataAccess.Models.IngredientDataModel", "Ingredient")
                        .WithMany("Instructions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");

                    b.Navigation("Flag");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("DataAccess.Models.LinkDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.LinkTypeDataModel", "Type")
                        .WithMany("Links")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("DataAccess.Models.ViewsDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkDataModel", "Drink")
                        .WithOne("ViewsData")
                        .HasForeignKey("DataAccess.Models.ViewsDataModel", "Views")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");
                });

            modelBuilder.Entity("DrinkBadgeDataModelDrinkDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkBadgeDataModel", null)
                        .WithMany()
                        .HasForeignKey("BadgesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.DrinkDataModel", null)
                        .WithMany()
                        .HasForeignKey("DrinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrinkDataModelDrinkTagDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkDataModel", null)
                        .WithMany()
                        .HasForeignKey("DrinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.DrinkTagDataModel", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrinkDataModelGarnishDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkDataModel", null)
                        .WithMany()
                        .HasForeignKey("DrinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.GarnishDataModel", null)
                        .WithMany()
                        .HasForeignKey("GarnishesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IngredientDataModelIngredientTagDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.IngredientDataModel", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.IngredientTagDataModel", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IngredientDataModelLinkDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.IngredientDataModel", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.LinkDataModel", null)
                        .WithMany()
                        .HasForeignKey("LinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InstructionDataModelInstructionTagDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.InstructionDataModel", null)
                        .WithMany()
                        .HasForeignKey("InstructionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.InstructionTagDataModel", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.DrinkDataModel", b =>
                {
                    b.Navigation("Instructions");

                    b.Navigation("ViewsData")
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.DrinkTagType", b =>
                {
                    b.Navigation("DrinkTags");
                });

            modelBuilder.Entity("DataAccess.Models.FlagDataModel", b =>
                {
                    b.Navigation("Instructions");
                });

            modelBuilder.Entity("DataAccess.Models.GlassDataModel", b =>
                {
                    b.Navigation("Drinks");
                });

            modelBuilder.Entity("DataAccess.Models.IceDataModel", b =>
                {
                    b.Navigation("Drinks");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientDataModel", b =>
                {
                    b.Navigation("Instructions");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientTypeDataModel", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("DataAccess.Models.LinkTypeDataModel", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("DataAccess.Models.RimDataModel", b =>
                {
                    b.Navigation("Drinks");
                });
#pragma warning restore 612, 618
        }
    }
}
