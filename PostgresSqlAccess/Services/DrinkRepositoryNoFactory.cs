using DataAccess.Context;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Services
{
	public class DrinkRepositoryNoFactory
	{
		private readonly DrinkDBContext _drinkDBContext;

		public DrinkRepositoryNoFactory(DrinkDBContext drinkDBContext)
		{
			this._drinkDBContext = drinkDBContext;
		}

		public void AddDrink(DrinkDataModel drink)
		{
			_drinkDBContext.Drinks.Add(drink);
			_drinkDBContext.SaveChanges();
		}

		public DrinkDataModel GetDrinkById(int id)
		{
			return _drinkDBContext.Drinks.SingleOrDefault(drink => drink.Id == id)!;
		}

		public DrinkDataModel GetDrinkByName(string name)
		{
			try
			{
				return _drinkDBContext.Drinks.FirstOrDefault(drink => drink.Name == name);
			}
			catch
			{
				return new DrinkDataModel();
			}
		}

		public List<DrinkDataModel> GetDrinks() 
		{

			return _drinkDBContext.Drinks.ToList();
		}

		public List<DrinkDataModel> GetAllDrinksByName(string name)
		{

			try
			{
				return _drinkDBContext.Drinks.Where(n => n.Name.Contains(name)).ToList();
			}
			catch (Exception ex)
			{
				return new List<DrinkDataModel>();
			}
		}

		public void UpdateDrink(DrinkDataModel drink)
		{

			_drinkDBContext.Update(drink);
			_drinkDBContext.SaveChanges();

		}

		public void DeleteDrink(int id)
		{
			_drinkDBContext.Remove(_drinkDBContext.Drinks.SingleOrDefault(drink => drink.Id == id)!);
			_drinkDBContext.SaveChanges();
		}
	}
}
