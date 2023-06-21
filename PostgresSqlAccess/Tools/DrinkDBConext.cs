﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Tools
{
	public class DrinkDBConext : DbContext
	{
		public DbSet<DrinkDataModel> Drinks { get; set; }

		public DbSet<DrinkTagsDataModel> DrinkTags { get; set; }
		public DbSet<InstructionDataModel> Instructions { get; set; }
		public DbSet<InstructionTagDataModel> InstructionTags { get; set; }
		
		public DbSet<IngredientDataModel> Ingredients { get; set; }

		public DbSet<IngredientTagDataModel> IngredientsTags { get; set;}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Username=postgres;Password=2244;Host=172.31.48.1;Port=5432;DataBase=Book;Pooling=true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DrinkDataModel>()
				.HasMany(e => e.Tags)
				.WithMany(e => e.Drinks);
			modelBuilder.Entity<DrinkDataModel>()
				.HasMany(e => e.Instructions).WithOne(e => e.Drink);
			modelBuilder.Entity<InstructionDataModel>()
				.HasOne(e => e.Ingredient)
				.WithMany(e => e.Instructions);
			modelBuilder.Entity<InstructionDataModel>()
				.HasMany(e => e.Tags)
				.WithMany(e => e.Instructions);
			modelBuilder.Entity<IngredientDataModel>()
				.HasOne(e => e.IngredientType)
				.WithMany(e => e.Ingredients);
			modelBuilder.Entity<IngredientDataModel>()
				.HasMany(e => e.Tags)
				.WithMany(e => e.Ingredients);

		}

	}
}
