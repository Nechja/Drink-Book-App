﻿// <auto-generated />
using System;
using DataAccess.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DrinkDBConext))]
    [Migration("20230620231646_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.DrinkDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Garnish")
                        .HasColumnType("text");

                    b.Property<string>("Ice")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("DataAccess.Models.DrinkTagsDataModel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("DrinkTags");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("IngredientTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IngredientTypeId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientTagDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IngredientsTags");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientTypeDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IngredientTypeDataModel");
                });

            modelBuilder.Entity("DataAccess.Models.InstructionDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DrinkId")
                        .HasColumnType("integer");

                    b.Property<int>("IngredientId")
                        .HasColumnType("integer");

                    b.Property<int?>("Oz")
                        .HasColumnType("integer");

                    b.Property<string>("Special")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Instructions");
                });

            modelBuilder.Entity("DataAccess.Models.InstructionTagDataModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("InstructionTags");
                });

            modelBuilder.Entity("DrinkDataModelDrinkTagsDataModel", b =>
                {
                    b.Property<int>("DrinksId")
                        .HasColumnType("integer");

                    b.Property<int>("Tagsid")
                        .HasColumnType("integer");

                    b.HasKey("DrinksId", "Tagsid");

                    b.HasIndex("Tagsid");

                    b.ToTable("DrinkDataModelDrinkTagsDataModel");
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

                    b.HasOne("DataAccess.Models.IngredientDataModel", "Ingredient")
                        .WithMany("Instructions")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drink");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("DrinkDataModelDrinkTagsDataModel", b =>
                {
                    b.HasOne("DataAccess.Models.DrinkDataModel", null)
                        .WithMany()
                        .HasForeignKey("DrinksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.DrinkTagsDataModel", null)
                        .WithMany()
                        .HasForeignKey("Tagsid")
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
                });

            modelBuilder.Entity("DataAccess.Models.IngredientDataModel", b =>
                {
                    b.Navigation("Instructions");
                });

            modelBuilder.Entity("DataAccess.Models.IngredientTypeDataModel", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
