using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		using(var context = _dbContextFactory.CreateDbContext())
		{
			context.Drinks.Add(drink);
			context.SaveChanges();
		}
	}

	public DrinkDataModel GetDrinkById(int id)
	{
		using(var context = _dbContextFactory.CreateDbContext())
		{
			try
			{
				return context.Drinks.Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
					.SingleOrDefault(drink => drink.Id == id)!;
			}
			catch
			{
				return new DrinkDataModel();
			}
			
		}
	}

	public DrinkDataModel GetDrinkByName(string name)
	{
		using(var context = _dbContextFactory.CreateDbContext())
		{
			return context.Drinks.FirstOrDefault(drink => drink.Name == name)!;
		}
	}


	public List<DrinkDataModel> GetDrinks()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.Drinks.ToList();
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
		using(var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(drink);
			context.SaveChanges();
		}
	}

	public void DeleteDrink(int id)
	{
		using(var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(context.Drinks.SingleOrDefault(drink => drink.Id == id)!);
			context.SaveChanges();
		}
	}

    //Ingredient types


    public List<IngredientTypeDataModel> GetIngredientTypes()
    {
        using (var context = _dbContextFactory.CreateDbContext())
        {
            return context.IngredientTypes.ToList();
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
            context.Remove(context.IngredientTypes.SingleOrDefault(t => t.Id == id)!);
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
		using(var context = _dbContextFactory.CreateDbContext())
		{
			context.IngredientsTags.Add(ingredientTag);
			context.SaveChanges();
		}
	}

	public List<IngredientDataModel> GetAllIngredient()
	{
		using(var context = _dbContextFactory.CreateDbContext())
		{
			return context.Ingredients.Include(i => i.IngredientType).Include(i => i.Tags).ToList();
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

			foreach(var tag in ingredient.Tags)
			{
				//prevent duplication
				if(!context.IngredientsTags.Any(i => i.Value == tag.Value))
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
	public List<ServingFlagDataModel> GetServingFlags()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.ServingFlags.ToList();
		}
	}

	public List<ShakerFlagDataModel> GetShakerFlags()
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			return context.ShakerFlags.ToList();
		}
	}

	public void AddShakerFlag(ShakerFlagDataModel shaker)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.ShakerFlags.Add(shaker);
			context.SaveChanges();
		}
	}

	public void AddServingFlag(ServingFlagDataModel Serving)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.ServingFlags.Add(Serving);
			context.SaveChanges();
		}
	}

	public void UpdateShakerFlag(ShakerFlagDataModel shaker)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(shaker);
			context.SaveChanges();
		}
	}

	public void UpdateServingFlag(ServingFlagDataModel Serving)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Update(Serving);
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


}
