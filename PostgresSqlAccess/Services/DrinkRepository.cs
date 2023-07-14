using DataAccess.Context;
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
				if (instruction.Flag.id != 0)
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
					.Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
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
			var updater = context.Drinks.Include(e => e.Garnishes).FirstOrDefault(d => d.Id == drink.Id);
			updater.Name = drink.Name;
			updater.Ice = drink.Ice;
			updater.Rim = drink.Rim;
			updater.Garnishes.Clear();
			context.ChangeTracker.DetectChanges();
			var debug1 = context.ChangeTracker.DebugView.ShortView;
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
			updater.Instructions.Clear();
			foreach (var instruction in drink.Instructions)
			{
				var existingInstruction = context.Instructions.FirstOrDefault(i => i.Id == instruction.Id);
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
			try
			{
				context.SaveChanges();
			}
			catch(Exception e) 
			{ 
				var ex = e; 
			}


		}
	}

	public void DeleteDrink(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.Drinks.SingleOrDefault(drink => drink.Id == id)!);
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
}