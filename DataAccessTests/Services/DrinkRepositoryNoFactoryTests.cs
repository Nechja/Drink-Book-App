using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Models;

namespace DataAccess.Services.Tests
{
	[TestClass()]
	public class DrinkRepositoryNoFactoryTests
	{
		private DrinkDataModel _drink;

		[TestInitialize]
		public void SetUp()
		{
			IngredientTypeDataModel ingredientType = new IngredientTypeDataModel();
			IngredientTagDataModel ingredientTag = new IngredientTagDataModel();
			IngredientDataModel ingredient = new IngredientDataModel();
			InstructionDataModel instruction = new InstructionDataModel();
			InstructionTagDataModel instructionTag = new InstructionTagDataModel();
			DrinkDataModel drink = new DrinkDataModel();
			DrinkTagDataModel drinkTag = new DrinkTagDataModel();

			//make some rum
			ingredientType.Name = "Rum";
			ingredient.Name = "Sailor Jerry's";
			ingredientTag.Value = "Spiced Rum";
			ingredient.Tags.Add(ingredientTag);
			ingredient.IngredientType = ingredientType;

			instructionTag.Value = "Free Pour";

			instruction.Ingredient = ingredient;
			instruction.Oz = 2;
			instruction.Tags.Add(instructionTag);

			drink.Instructions.Add(instruction);

			IngredientTypeDataModel ingredientType2 = new IngredientTypeDataModel();
			IngredientTagDataModel ingredientTag2 = new IngredientTagDataModel();
			IngredientDataModel ingredient2 = new IngredientDataModel();
			InstructionDataModel instruction2 = new InstructionDataModel();
			InstructionTagDataModel instructionTag2 = new InstructionTagDataModel();

			ingredientType2.Name = "Soda";
			ingredient2.Name = "Coke";
			ingredientTag2.Value = "Soda Gun";;
			ingredient2.Tags.Add(ingredientTag);
			ingredient2.IngredientType = ingredientType;

			instruction2.Ingredient = ingredient;
			instruction2.Oz = null;
			instruction2.Special = "Fill";
			instruction2.Tags.Clear();

			drink.Instructions.Add(instruction2);

			drinkTag.Value = "Basic";

			drink.Name = "Rum & Coke";
			drink.Tags.Add(drinkTag);

			this._drink = drink;




		}

		[TestMethod()]
		public void DrinkRepositoryNoFactoryTest()
		{
			 DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void AddDrinkTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				repo.AddDrink(_drink);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void GetDrinkByIdTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				DrinkDataModel drink = repo.GetDrinkById(1);
				Assert.IsNotNull(drink);
				Assert.IsTrue(drink.Id == 1);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void GetDrinkByNameTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				DrinkDataModel drink = repo.GetDrinkByName(_drink.Name);
				Assert.IsNotNull(drink);
				Assert.IsTrue(drink.Name == _drink.Name);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void GetDrinksTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				var list = repo.GetDrinks();
				Assert.IsNotNull(list);
				Assert.IsTrue(list.Count >= 1);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void GetDrinksByNameTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				var list = repo.GetAllDrinksByName("Ru");
				Assert.IsNotNull(list);
				Assert.IsTrue(list.Count >= 1);
				var list2 = repo.GetAllDrinksByName("Zorgglabe");
				Assert.IsTrue(list2.Count == 0);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void UpdateDrinkTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				DrinkDataModel drink = repo.GetDrinkByName(_drink.Name);
				drink.Name = "Cuba Libre";
				repo.UpdateDrink(drink);
				DrinkDataModel moddeddrink = repo.GetDrinkByName(drink.Name);
				Assert.IsNotNull(moddeddrink);
				if (drink != moddeddrink)
				{
					Assert.Fail("Drink did not update.");
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[TestMethod()]
		public void DeleteDrinkTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
				repo.DeleteDrink(1);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}