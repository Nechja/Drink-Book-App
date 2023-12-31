﻿using DataAccess.Context;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services;

public class DrinkRepository
{
	private readonly IDbContextFactory<DrinkDBContext> _dbContextFactory;

	public DrinkRepository(IDbContextFactory<DrinkDBContext> dbContextFactory)
	{
		this._dbContextFactory = dbContextFactory;
	}
	public void AddDrink(DrinkDataModel drink)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			//repackthatdrink


			//glass
			var glass = context.Glasses.FirstOrDefault(g => g.Id == drink.Glass.Id!);
			drink.Glass = glass;

			//tags
			var drinktags = new List<DrinkTagDataModel>();
			foreach (var tag in drink.Tags)
			{
				if (tag.Id == 0)
				{
					AddDrinkTag(tag);
				}
				var existingtag = context.DrinkTags.SingleOrDefault(i => i.Value == tag.Value);
				if (existingtag != null)
				{
					drinktags.Add(existingtag);
				}
			}
			drink.Tags.Clear();
			drink.Tags = drinktags;

			//ice
			if (drink.Ice != null)
			{
				var ice = context.IceTypes.FirstOrDefault(i => i.Id == drink.Ice.Id!);
				drink.Ice = ice;
			}

			//
			if (drink.Rim != null)
			{
				var rim = context.RimTypes.FirstOrDefault(r => r.Id == drink.Rim.Id!);
				drink.Rim = rim;
			}


			//Garnishes
			var garnishtags = new List<GarnishDataModel>();
			foreach (var tag in drink.Garnishes)
			{
				var garnish = context.GarnishTypes.SingleOrDefault(i => i.Value == tag.Value);
				if (garnish != null)
				{
					garnishtags.Add(garnish);
				}
			}
			drink.Garnishes.Clear();
			drink.Garnishes = garnishtags;


			//unwinding and repacking instructions
			var ins = new List<InstructionDataModel>();
			foreach (InstructionDataModel instruction in drink.Instructions)
			{
				if (instruction.Flag != null)
				{
					var flag = context.Flags.FirstOrDefault(f => f.id == instruction.Flag.id);
					instruction.Flag = flag;
				}
				else { instruction.Flag = null; }

				var ingredient = context.Ingredients.FirstOrDefault(i => i.Id == instruction.Ingredient.Id);
				instruction.Ingredient = ingredient;

				var instags = new List<InstructionTagDataModel>();
				foreach (var tag in instruction.Tags)
				{
					if (tag.Id == 0)
					{
						AddInstructionTag(tag);
					}
					var existingtag = context.InstructionTags.SingleOrDefault(i => i.Value == tag.Value);
					if (existingtag != null)
					{
						instags.Add(existingtag);
					}
				}
				instruction.Tags = instags;
				ins.Add(instruction);
			}
			drink.Instructions.Clear();
			drink.Instructions = ins;

			context.Drinks.Add(drink);
			context.SaveChanges();
		}
	}

	private void AddInstructionTag(InstructionTagDataModel tag)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.InstructionTags.Add(tag);
			context.SaveChanges();
		}
	}

	public void AddDrinkTag(DrinkTagDataModel drinkTag)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.DrinkTags.Add(drinkTag);
			context.SaveChanges();
		}
	}

	public List<DrinkTagDataModel> GetDrinkTags()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.DrinkTags.ToList();
		}
	}

	public DrinkDataModel GetDrinkById(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			try
			{
				var drink = context.Drinks
					.Include(i => i.Glass)
					.Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
					.Include(i => i.Instructions)
					.ThenInclude(i => i.Flag)
					.Include(i => i.Garnishes)
					.Include(i => i.Ice)
					.Include(i => i.Rim)
                    .Include(i => i.Mod)
                    .Include(i => i.Tags)
					.ThenInclude(i => i.TagType)
					.SingleOrDefault(drink => drink.Id == id)!;

				return drink;
			}
			catch
			{
				return new DrinkDataModel();
			}

		}
	}

	public DrinkDataModel GetDrinkByName(string name)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Drinks.FirstOrDefault(drink => drink.Name == name)!;
		}
	}


	public List<DrinkDataModel> GetDrinks()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Drinks
					.Include(i => i.Glass)
					.Include(i => i.Tags)
					.ThenInclude(i => i.TagType)
					.Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
                    .Include(i => i.Mod)
                    .Include(i => i.Instructions)
					.ThenInclude(i => i.Flag).ToList();
		}
	}

    public List<DrinkDataModel> GetDrinksAudit()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            return context.Drinks.IgnoreQueryFilters()
				.ToList();
        }
    }

    public List<DrinkDataModel> GetAllDrinks()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Drinks.IgnoreQueryFilters()
					.Include(i => i.Glass)
					.Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
					.Include(i => i.Instructions)
					.ThenInclude(i => i.Flag).ToList();
		}
	}

	public void UndoSoftDeleteDrink(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			var updater = context.Drinks.IgnoreQueryFilters().FirstOrDefault(d => d.Id == id);
			if (updater != null)
			{
				updater.IsDeleted = false;
				updater.FINALDELETE = false;
				updater.DeletedAt = null;
				context.SaveChanges();
			}
		}
	}

	public List<DrinkDataModel> GetAllDrinksByName(string name)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			try
			{
				return context.Drinks.Where(n => n.Name.Contains(name)).ToList();
			}
			catch
			{
				return new List<DrinkDataModel>();
			}
		}

	}



	public async Task UpdateDrink(DrinkDataModel drink)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			var updater = context.Drinks
				.Include(e => e.Tags)
				.Include(e => e.Garnishes).SingleOrDefault(d => d.Id == drink.Id);
			updater.Name = drink.Name;
			updater.Ice = drink.Ice;
			updater.Rim = drink.Rim;
			updater.Verification = drink.Verification;
			updater.Link = drink.Link;
			updater.Garnishes.Clear();
			context.SaveChanges();
			foreach (var garnish in drink.Garnishes)
			{
				var attachedGarnish = context.GarnishTypes.Local.FirstOrDefault(g => g.Id == garnish.Id)
					?? context.GarnishTypes.Attach(garnish).Entity;
				updater.Garnishes.Add(attachedGarnish);
			}
			context.ChangeTracker.DetectChanges();
			var debug2 = context.ChangeTracker.DebugView.ShortView;
			context.SaveChanges();
            updater.Glass = context.Glasses.FirstOrDefault(e => e.Id == drink.Glass.Id);
            updater.Instructions.Clear();
			foreach (var instruction in drink.Instructions)
			{
				var existingInstruction = context.Instructions.Include(e => e.Flag).FirstOrDefault(i => i.Id == instruction.Id);
				if (existingInstruction != null)
				{
					// Update existing instruction if it has changed
					if (existingInstruction.Oz != instruction.Oz || existingInstruction.Special != instruction.Special ||
						existingInstruction.DisplayWeight != instruction.DisplayWeight)
					{
						existingInstruction.Oz = instruction.Oz;
						existingInstruction.Special = instruction.Special;
						existingInstruction.DisplayWeight = instruction.DisplayWeight;
						context.Entry(existingInstruction).State = EntityState.Modified;
					}
					if(instruction.Flag == null)
					{
						existingInstruction.Flag = null;
					}

					else if(existingInstruction.Flag != instruction.Flag)
					{
						existingInstruction.Flag = context.Flags.FirstOrDefault(e => e.id == instruction.Flag.id);
						context.Entry(existingInstruction).State = EntityState.Modified;
					}

					if (existingInstruction.Ingredient != instruction.Ingredient)
					{
						existingInstruction.Ingredient = context.Ingredients.FirstOrDefault(e => e.Id == instruction.Ingredient.Id);
						context.Entry(existingInstruction).State = EntityState.Modified;
					}
				}
				else
				{
					var newInstruction = new InstructionDataModel
					{
						Oz = instruction.Oz,
						Special = instruction.Special,
						DisplayWeight = instruction.DisplayWeight,
						Ingredient = context.Ingredients.Attach(instruction.Ingredient).Entity,
					};

					if (instruction.Flag != null)
					{
						newInstruction.Flag = context.Flags.Attach(instruction.Flag).Entity;
					}

					updater.Instructions.Add(newInstruction);

				}

			}
			var ogdrink = context.Drinks
				.Include(e => e.Instructions).SingleOrDefault(d => d.Id == drink.Id);
			foreach (var instruction in ogdrink.Instructions)
			{
				var check = drink.Instructions.FirstOrDefault(i => i.Id == instruction.Id);
				if (check == null)
				{
					context.Entry(instruction).State = EntityState.Deleted;
				}
			}
			try
			{
				context.SaveChanges();
			}
			catch(Exception e) 
			{ 
				var ex = e; 
			}

			updater.Tags.Clear();
			foreach (var tag in drink.Tags)
			{
				var existingTag = context.DrinkTags.FirstOrDefault(t => t.Id == tag.Id);
				if (existingTag != null)
				{
					updater.Tags.Add(existingTag);
				}
				else
				{
					var newTag = new DrinkTagDataModel(tag.Value);
					updater.Tags.Add(newTag);
				}
			}
			context.SaveChanges();
		}
	}

	public void DeleteDrink(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.Drinks.IgnoreQueryFilters().SingleOrDefault(drink => drink.Id == id)!);
			context.SaveChanges();
		}
	}

	public void DeleteInstruction(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.Instructions.Single(i => i.Id == id)!);
			context.SaveChanges();
		}
	}

	//Ingredient types


	public List<IngredientTypeDataModel> GetIngredientTypes()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.IngredientTypes.Include(i => i.Ingredients).ToList();
		}
	}

    public List<IngredientTypeDataModel> GetIngredientTypesFullChain()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            return context.IngredientTypes
				.Include(i => i.Ingredients)
				.ThenInclude(i => i.Instructions)
				.ToList();
        }
    }

    public void UpdateIngredientType(IngredientTypeDataModel ingredientType)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(ingredientType);
			context.SaveChanges();
		}
	}

	public void AddIngredientType(IngredientTypeDataModel ingredientType)
	{

		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.IngredientTypes.Add(ingredientType);
			context.SaveChanges();
		}


	}

	public void DeleteIngredientType(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.IngredientTypes.Single(t => t.Id == id)!);
			context.SaveChanges();
		}
	}

	//Ingredients

	public List<IngredientTagDataModel> GetIngredientTags()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.IngredientsTags.ToList();
		}
	}

	public void AddIngredientTag(IngredientTagDataModel ingredientTag)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.IngredientsTags.Add(ingredientTag);
			context.SaveChanges();
		}
	}

	public List<IngredientDataModel> GetAllIngredient()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Ingredients.Include(i => i.IngredientType).Include(i => i.Tags).ToList();
		}
	}

    public List<IngredientDataModel> GetAllIngredientFullChain()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            return context.Ingredients
				.Include(i => i.IngredientType)
				.Include(i => i.Tags)
				.Include(i => i.Instructions)
				.ThenInclude(i => i.Drink)
				.ToList();
        }
    }

    public void AddIngredient(IngredientDataModel ingredient)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			if (ingredient.IngredientType.Id == 0)
			{
				AddIngredientType(ingredient.IngredientType);
			}
			var exsitingtype = context.IngredientTypes.SingleOrDefault(i => i.Name == ingredient.IngredientType.Name);
			ingredient.IngredientType = exsitingtype;

			List<IngredientTagDataModel> dbtags = new List<IngredientTagDataModel>();

			foreach (var tag in ingredient.Tags)
			{
				//prevent duplication
				if (!context.IngredientsTags.Any(i => i.Value == tag.Value))
				{
					if (tag.Id == 0)
					{
						AddIngredientTag(tag);
					}
				}
				var existingtag = context.IngredientsTags.SingleOrDefault(i => i.Value == tag.Value);
				if (existingtag != null)
				{
					dbtags.Add(existingtag);
				}
			}
			ingredient.Tags.Clear();
			ingredient.Tags = dbtags;
			context.Ingredients.Add(ingredient);
			context.SaveChanges();
		}
	}

	public void UpdateIngredient(IngredientDataModel ingredient)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(ingredient);
			context.SaveChanges();
		}
	}

	public object GetIngredient()
	{
		throw new NotImplementedException();
	}

	//flags

	public List<FlagDataModel> GetFlags()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Flags.ToList();
		}
	}

	public void AddFlag(FlagDataModel shaker)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Flags.Add(shaker);
			context.SaveChanges();
		}
	}


	public void UpdateFlag(FlagDataModel shaker)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(shaker);
			context.SaveChanges();
		}
	}

	public void DeleteFlag(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.Flags.Single(t => t.id == id)!);
			context.SaveChanges();
		}
	}







	//instructions
	public List<InstructionTagDataModel> GetInstructionTags()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.InstructionTags.ToList();
		}
	}

	//glassware
	public List<GlassDataModel> GetGlassware()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Glasses.ToList();
		}
	}

	public void AddGlass(GlassDataModel glass)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Glasses.Add(glass);
			context.SaveChanges();
		}
	}

	public void UpdateGlass(GlassDataModel glass)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(glass);
			context.SaveChanges();
		}
	}

	public void DeleteGlass(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.Glasses.Single(t => t.Id == id)!);
			context.SaveChanges();
		}
	}

	//ice

	public List<IceDataModel> GetIceTypes()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.IceTypes.ToList();
		}
	}

	public void AddIce(IceDataModel ice)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.IceTypes.Add(ice);
			context.SaveChanges();
		}
	}

	public void UpdateIce(IceDataModel ice)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(ice);
			context.SaveChanges();
		}
	}

	public void DeleteIce(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.IceTypes.Single(t => t.Id == id)!);
			context.SaveChanges();
		}
	}

	//garnish

	public List<GarnishDataModel> GetGarnishTypes()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.GarnishTypes.ToList();
		}
	}

	public void AddGarnish(GarnishDataModel garnish)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.GarnishTypes.Add(garnish);
			context.SaveChanges();
		}
	}

	public void UpdateGarnish(GarnishDataModel garnish)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(garnish);
			context.SaveChanges();
		}
	}

	public void DeleteGarnish(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.GarnishTypes.Single(t => t.Id == id)!);
			context.SaveChanges();
		}
	}


	//rims

	public List<RimDataModel> GetRimTypes()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.RimTypes.ToList();
		}
	}

	public void AddRim(RimDataModel rim)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.RimTypes.Add(rim);
			context.SaveChanges();
		}
	}

	public void UpdateRim(RimDataModel rim)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(rim);
			context.SaveChanges();
		}
	}

	public void DeleteRim(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.RimTypes.Single(t => t.Id == id)!);
			context.SaveChanges();
		}
	}
}