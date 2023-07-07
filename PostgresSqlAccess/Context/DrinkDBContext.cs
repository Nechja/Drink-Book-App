using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Context
{
	public class DrinkDBContext : DbContext
	{
		public DbSet<DrinkDataModel> Drinks { get; set; }

		public DbSet<DrinkTagDataModel> DrinkTags { get; set; }
		public DbSet<InstructionDataModel> Instructions { get; set; }
		public DbSet<InstructionTagDataModel> InstructionTags { get; set; }
		
		public DbSet<IngredientDataModel> Ingredients { get; set; }

		public DbSet<GlassDataModel> Glasses { get; set; }
		public DbSet<IngredientTypeDataModel>  IngredientTypes { get; set; }

		public DbSet<IngredientTagDataModel> IngredientsTags { get; set;}

		public DbSet<FlagDataModel> Flags { get; set; }



		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Username=postgres;Password=2244;Host=172.31.48.1;Port=5432;DataBase=DrinkBook;Pooling=true;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<DrinkDataModel>()
				.HasMany(e => e.Tags)
				.WithMany(e => e.Drinks);
			modelBuilder.Entity<DrinkDataModel>()
				.HasMany(e => e.Instructions).WithOne(e => e.Drink);
			modelBuilder.Entity<DrinkDataModel>()
				.HasOne(e => e.Mod);
			modelBuilder.Entity<DrinkDataModel>()
				.HasIndex(p => p.Name)
				.IncludeProperties(p => p.Id);
			modelBuilder.Entity<DrinkDataModel>()
				.HasOne(p => p.Glass).WithMany(p => p.Drinks);

			modelBuilder.Entity<InstructionDataModel>()
				.HasOne(e => e.Ingredient)
				.WithMany(e => e.Instructions);
			modelBuilder.Entity<InstructionDataModel>()
				.HasOne(e => e.Flag)
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
