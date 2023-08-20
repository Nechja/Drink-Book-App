using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Services;

public class DrinkRepositoryAsync
{
	private readonly IDbContextFactory<DrinkDBContext> _dbContextFactory;

	public DrinkRepositoryAsync(IDbContextFactory<DrinkDBContext> dbContextFactory)
	{
		this._dbContextFactory = dbContextFactory;
	}
	public async Task AddDrink(DrinkDataModel drink)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
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

			await context.Drinks.AddAsync(drink);
			await context.SaveChangesAsync();
		}
	}

	private async Task AddInstructionTag(InstructionTagDataModel tag)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.InstructionTags.AddAsync(tag);
			await context.SaveChangesAsync();
		}
	}

	public async Task AddDrinkTag(DrinkTagDataModel drinkTag)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			drinkTag.TagType = await context.DrinkTagTypes.FirstOrDefaultAsync(i => i == drinkTag.TagType);
			await context.DrinkTags.AddAsync(drinkTag);
			await context.SaveChangesAsync();
		}
	}

	public async Task<List<DrinkTagDataModel>> GetDrinkTags()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.DrinkTags.ToListAsync();
		}
	}

	public async Task UpdateDrinkTag(DrinkTagDataModel tag)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
            tag.TagType = await context.DrinkTagTypes.FirstOrDefaultAsync(i => i == tag.TagType);
            context.Update(tag);
			await context.SaveChangesAsync();
		}
	}

	public async Task<List<DrinkTagType>> GetDrinkTagTypes()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.DrinkTagTypes.ToListAsync();
		}
	}

	public async Task<DrinkDataModel> GetDrinkById(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			try
			{
				var drink = await context.Drinks
					.Include(i => i.Glass)
                    .Include(i => i.Mod)
                    .Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
					.Include(i => i.Instructions)
					.ThenInclude(i => i.Flag)
					.Include(i => i.Garnishes)
					.Include(i => i.Ice)
					.Include(i => i.Rim)
                    .Include(i => i.Tags)
					.ThenInclude(i => i.TagType)
					.SingleOrDefaultAsync(drink => drink.Id == id)!;

				if (drink == null) return new DrinkDataModel();
				return drink;
			}
			catch
			{
				return new DrinkDataModel();
			}

		}
	}

	public async Task<DrinkDataModel> GetDrinkByName(string name)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			try
			{
				var drink = await context.Drinks.FirstOrDefaultAsync(drink => drink.Name == name);
				if (drink == null) return new DrinkDataModel();
				return drink;

			}
			catch
			{
				return new DrinkDataModel();
			}

		}
	}


	public async Task<List<DrinkDataModel>> GetDrinks()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			var drinks = await context.Drinks
                    .Include(i => i.Glass)
                    .Include(i => i.Tags)
                    .ThenInclude(i => i.TagType)
                    .Include(r => r.Instructions)
                    .ThenInclude(i => i.Ingredient)
                    .ThenInclude(i => i.IngredientType)
                    .Include(i => i.Instructions)
                    .ThenInclude(i => i.Flag).ToListAsync();
            return drinks.OrderBy(i => i.Name).ToList();
		}
	}

	public async Task<List<DrinkDataModel>> GetDrinksAudit()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.Drinks.IgnoreQueryFilters()
				.ToListAsync();
		}
	}

	public async Task<List<DrinkDataModel>> GetAllDrinks()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.Drinks.IgnoreQueryFilters()
					.Include(i => i.Glass)
					.Include(r => r.Instructions)
					.ThenInclude(i => i.Ingredient)
					.ThenInclude(i => i.IngredientType)
					.Include(i => i.Instructions)
					.ThenInclude(i => i.Flag).ToListAsync();
		}
	}

	public async Task UndoSoftDeleteDrink(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			var updater = await context.Drinks.IgnoreQueryFilters().FirstOrDefaultAsync(d => d.Id == id);
			if (updater != null)
			{
				updater.IsDeleted = false;
				updater.FINALDELETE = false;
				updater.DeletedAt = null;
				await context.SaveChangesAsync();
			}
		}
	}

	public async Task<List<DrinkDataModel>> GetAllDrinksByName(string name)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			try
			{
				return await context.Drinks.Where(n => n.Name.Contains(name)).ToListAsync();
			}
			catch
			{
				return new List<DrinkDataModel>();
			}
		}

	}



	public async Task UpdateDrink(DrinkDataModel drink)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			var updater = await context.Drinks.Include(e => e.Tags)
				.Include(e => e.Garnishes).FirstOrDefaultAsync(d => d.Id == drink.Id);
			updater.Name = drink.Name;
			updater.Ice = drink.Ice;
			updater.Rim = drink.Rim;
			updater.Garnishes.Clear();
			context.ChangeTracker.DetectChanges();
			var debug1 = context.ChangeTracker.DebugView.ShortView;
			await context.SaveChangesAsync();
			foreach (var garnish in drink.Garnishes)
			{
				var attachedGarnish = context.GarnishTypes.Local.FirstOrDefault(g => g.Id == garnish.Id)
					?? context.GarnishTypes.Attach(garnish).Entity;
				updater.Garnishes.Add(attachedGarnish);
			}
			context.ChangeTracker.DetectChanges();
			var debug2 = context.ChangeTracker.DebugView.ShortView;
			context.SaveChangesAsync();
			


			updater.Instructions.Clear();
			foreach (var instruction in drink.Instructions)
			{
				var existingInstruction = await context.Instructions.Include(e => e.Ingredient).FirstOrDefaultAsync(i => i.Id == instruction.Id);
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
					if(existingInstruction.Ingredient != instruction.Ingredient)
					{
						existingInstruction.Ingredient = await context.Ingredients.FirstOrDefaultAsync(e => e.Id == instruction.Ingredient.Id);
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
				await context.SaveChangesAsync();
			}
			catch (Exception e)
			{
				var ex = e;
			}

			updater.Tags.Clear();
			foreach (var tag in drink.Tags)
			{
				var existingTag = await context.DrinkTags.FirstOrDefaultAsync(t => t.Id == tag.Id);
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
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteDrink(int id)
	{
		using (var context = _dbContextFactory.CreateDbContext())
		{
			context.Remove(await context.Drinks.IgnoreQueryFilters().SingleOrDefaultAsync(drink => drink.Id == id)!);
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteInstruction(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(await context.Instructions.SingleAsync(i => i.Id == id)!);
			await context.SaveChangesAsync();
		}
	}

	//Ingredient types


	public async Task<List<IngredientTypeDataModel>> GetIngredientTypes()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.IngredientTypes.Include(i => i.Ingredients).ToListAsync();
		}
	}

	public async Task<List<IngredientTypeDataModel>> GetIngredientTypesFullChain()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.IngredientTypes
				.Include(i => i.Ingredients)
				.ThenInclude(i => i.Instructions)
				.ToListAsync();
		}
	}

	public async Task UpdateIngredientType(IngredientTypeDataModel ingredientType)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(ingredientType);
			await context.SaveChangesAsync();
		}
	}

	public async Task AddIngredientType(IngredientTypeDataModel ingredientType)
	{

		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.IngredientTypes.AddAsync(ingredientType);
			await context.SaveChangesAsync();
		}


	}

	public async Task DeleteIngredientType(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(context.IngredientTypes.Single(t => t.Id == id)!);
			await context.SaveChangesAsync();
		}
	}

	//Ingredients

	public async Task<List<IngredientTagDataModel>> GetIngredientTags()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.IngredientsTags.ToListAsync();
		}
	}

	public async Task AddIngredientTag(IngredientTagDataModel ingredientTag)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.IngredientsTags.AddAsync(ingredientTag);
			await context.SaveChangesAsync();
		}
	}

	public async Task<List<IngredientDataModel>> GetAllIngredient()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.Ingredients.Include(i => i.IngredientType).Include(i => i.Tags).ToListAsync();
		}
	}

	public async Task<List<IngredientDataModel>> GetAllIngredientFullChain()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.Ingredients
				.Include(i => i.IngredientType)
				.Include(i => i.Tags)
				.Include(i => i.Instructions)
				.ThenInclude(i => i.Drink)
				.ToListAsync();
		}
	}

	public async Task AddIngredient(IngredientDataModel ingredient)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			if (ingredient.IngredientType.Id == 0)
			{
				await AddIngredientType(ingredient.IngredientType);
			}
			var exsitingtype = await context.IngredientTypes.SingleOrDefaultAsync(i => i.Name == ingredient.IngredientType.Name);
			ingredient.IngredientType = exsitingtype;

			List<IngredientTagDataModel> dbtags = new List<IngredientTagDataModel>();

			foreach (var tag in ingredient.Tags)
			{
				//prevent duplication
				if (!context.IngredientsTags.Any(i => i.Value == tag.Value))
				{
					if (tag.Id == 0)
					{
						await AddIngredientTag(tag);
					}
				}
				var existingtag = await context.IngredientsTags.SingleOrDefaultAsync(i => i.Value == tag.Value);
				if (existingtag != null)
				{
					dbtags.Add(existingtag);
				}
			}
			ingredient.Tags.Clear();
			ingredient.Tags = dbtags;
			await context.Ingredients.AddAsync(ingredient);
			await context.SaveChangesAsync();
		}
	}

	public async Task UpdateIngredient(IngredientDataModel ingredient)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(ingredient);
			await context.SaveChangesAsync();
		}
	}

	public void GetIngredient()
	{
		throw new NotImplementedException();
	}

	//flags

	public async Task<List<FlagDataModel>> GetFlags()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.Flags.ToListAsync();
		}
	}

	public async Task AddFlag(FlagDataModel shaker)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Flags.AddAsync(shaker);
			await context.SaveChangesAsync();
		}
	}


	public async Task UpdateFlag(FlagDataModel shaker)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(shaker);
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteFlag(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(context.Flags.Single(t => t.id == id)!);
			await context.SaveChangesAsync();
		}
	}







	//instructions
	public async Task<List<InstructionTagDataModel>> GetInstructionTags()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.InstructionTags.ToListAsync();
		}
	}

	//glassware
	public async Task<List<GlassDataModel>> GetGlassware()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.Glasses.ToListAsync();
		}
	}

	public async Task AddGlass(GlassDataModel glass)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.Glasses.AddAsync(glass);
			await context.SaveChangesAsync();
		}
	}

	public async Task UpdateGlass(GlassDataModel glass)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(glass);
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteGlass(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(await context.Glasses.SingleAsync(t => t.Id == id)!);
			await context.SaveChangesAsync();
		}
	}

	//ice

	public async Task<List<IceDataModel>> GetIceTypes()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.IceTypes.ToListAsync();
		}
	}

	public async Task AddIce(IceDataModel ice)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.IceTypes.AddAsync(ice);
			await context.SaveChangesAsync();
		}
	}

	public async Task UpdateIce(IceDataModel ice)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(ice);
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteIce(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(context.IceTypes.Single(t => t.Id == id)!);
			await context.SaveChangesAsync();
		}
	}

	//garnish

	public async Task<List<GarnishDataModel>> GetGarnishTypes()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.GarnishTypes.ToListAsync();
		}
	}

	public async Task AddGarnish(GarnishDataModel garnish)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.GarnishTypes.AddAsync(garnish);
			await context.SaveChangesAsync();
		}
	}

	public async Task UpdateGarnish(GarnishDataModel garnish)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(garnish);
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteGarnish(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(await context.GarnishTypes.SingleAsync(t => t.Id == id)!);
			await context.SaveChangesAsync();
		}
	}


	//rims

	public async Task<List<RimDataModel>> GetRimTypes()
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			return await context.RimTypes.ToListAsync();
		}
	}

	public async Task AddRim(RimDataModel rim)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			await context.RimTypes.AddAsync(rim);
			await context.SaveChangesAsync();
		}
	}

	public async Task UpdateRim(RimDataModel rim)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Update(rim);
			await context.SaveChangesAsync();
		}
	}

	public async Task DeleteRim(int id)
	{
		using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			context.Remove(await context.RimTypes.SingleAsync(t => t.Id == id)!);
			await context.SaveChangesAsync();
		}
	}



	public async Task MakeUserLink(string user)
	{
        var unGuid = "";
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] un = sha256.ComputeHash(Encoding.ASCII.GetBytes(user));
            unGuid = Encoding.ASCII.GetString(un);
        }
        using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
            if (context.Users.SingleOrDefault(e => e.UserName == unGuid) == null)
            {
                UserDataModel userDataModel = new UserDataModel();
                userDataModel.SetUserName = user;
                //fix
                int i = 0;
                while (userDataModel.UserDisplayName == null)
                {
                    i++;
                    string newname = string.Empty;
                    if (i == 99)
                    {
                        newname = RandomName.NameWithNumbers;
                    }
                    else
                    {
                        newname = RandomName.Name;
                    }

                    if (context.Users.SingleOrDefault(e => e.UserDisplayName == newname) == null)
                    {
                        userDataModel.UserDisplayName = newname;
                    }


                    if (i == 100)
                    {
                        throw new Exception("Can not add new list names.");
                    }
                }
                userDataModel.UserDisplayName = RandomName.Name;
                await context.AddAsync(userDataModel);
                await context.SaveChangesAsync();

            }
        }	
	}

    public async Task<string> GetUserLink(string user)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            using (SHA256 sha256 = SHA256.Create())
            {
				byte[] un = sha256.ComputeHash(Encoding.ASCII.GetBytes(user));
				var unGuid = Encoding.ASCII.GetString(un);

                var User = await context.Users.SingleOrDefaultAsync(e => e.UserName == unGuid);

                if (User == null)
                {
                    await MakeUserLink(user);
                    User = await context.Users.SingleOrDefaultAsync(e => e.UserName == unGuid);
                    return User.UserDisplayName;
                }
                else
                {
                    return User.UserDisplayName;
                }
            }

        }
    }

    public async Task<UserDataModel> GetUser(string user)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] un = sha256.ComputeHash(Encoding.ASCII.GetBytes(user));
                var unGuid = Encoding.ASCII.GetString(un);

                var User = await context.Users.SingleOrDefaultAsync(e => e.UserName == unGuid);

                if (User == null)
                {
                    await MakeUserLink(user);
                    User = await context.Users.SingleOrDefaultAsync(e => e.UserName == unGuid);
					return User;
                }
                else
                {
					return User;
                }
            }

        }
    }
    public async Task NewList(string username)
	{
        using (var context = await _dbContextFactory.CreateDbContextAsync())
		{
			try
			{
                var user = await GetUser(username);
                UserDrinkListsDataModel drinklist = new UserDrinkListsDataModel();
				var lists = await GetLists(username);
				if(lists.Count >10) { return; }
                drinklist.Name = RandomName.ListName;
                drinklist.User = await context.Users.FirstOrDefaultAsync(e => e.Id == user.Id);
                await context.AddAsync(drinklist);
                await context.SaveChangesAsync();
            }
			catch
			{
                throw;
            }


		}

    }

	public async Task DeleteDrinkList(string username, int listId)
	{
		try
		{
			using(var context = await _dbContextFactory.CreateDbContextAsync())
			{
				var user = await GetUser(username);
				var list = await context.DrinkLists
					.Include(e => e.User)
					.FirstOrDefaultAsync(e => e.Id == listId);
				if(user.Id != list.User.Id) { return; }
				list.Drinks.Clear();
				context.Remove(list);
				context.Entry(list).State = EntityState.Deleted;
				await context.SaveChangesAsync();
			}
		}
		catch(Exception e)
		{
			throw;
		}
	}

    public async Task AppendDrinkList(string username,int listid, DrinkDataModel drink)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            var user = await GetUser(username);
			var drinklist = await context.DrinkLists.Include(e => e.User).SingleOrDefaultAsync(e => e.Id == listid);
			if (drinklist == null) { return; }
			if (user.Id != drinklist.User.Id) { return; }
			drinklist.Drinks.Add(await context.Drinks.SingleOrDefaultAsync(e => e.Id == drink.Id));
			context.Update(drinklist);
			await context.SaveChangesAsync();




        }

    }

    public async Task RemoveDrinkFromDrinkList(string username, int listid, int drinkid)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            var user = await GetUser(username);
            var drinklist = await context.DrinkLists.Include(e => e.User).Include(e => e.Drinks).SingleOrDefaultAsync(e => e.Id == listid);
            if (drinklist == null) { return; }
            if (user.Id != drinklist.User.Id) { return; }
            drinklist.Drinks.Remove(await context.Drinks.SingleOrDefaultAsync(e => e.Id == drinkid));
			context.Entry(drinklist).State = EntityState.Modified;
            context.Update(drinklist);
            await context.SaveChangesAsync();




        }
    }

    public async Task RenameDrinkList(int userid, int listid, string listname)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
            var user = await context.Users.SingleOrDefaultAsync(e => e.Id == userid);
            var drinklist = await context.DrinkLists.Include(e => e.User).SingleOrDefaultAsync(e => e.Id == listid);
            if (drinklist == null) { return; }
            if (user.Id != drinklist.User.Id) { return; }
			drinklist.Name = listname;
            context.Update(drinklist);
            await context.SaveChangesAsync();




        }

    }

    public async Task<UserDrinkListsDataModel> GetList(int id)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
			return await context.DrinkLists
				.Include(e => e.User)
				.Include(e => e.Drinks)
				.SingleOrDefaultAsync(e => e.Id == id);

        }

    }

    public async Task<List<UserDrinkListsDataModel>> GetLists(string username)
    {
        using (var context = await _dbContextFactory.CreateDbContextAsync())
        {
			var user = await GetUser(username);
			var lists = context.DrinkLists.Where(e => e.User.Id == user.Id).ToList();
			return lists;

        }

    }
}