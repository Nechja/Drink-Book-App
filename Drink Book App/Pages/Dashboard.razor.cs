using DataAccess.Services;
using Drink_Book_App.Data;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Linq;

namespace Drink_Book_App.Pages
{
	public partial class Dashboard
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		private List<(int count, string name)> IngredientTypesCounter = new List<(int count, string name)>();

		
		public double[] typeDataArray { get; set; }
		public string[] typeNamesArray { get; set; }

		private int Index = -1;

		protected override void OnInitialized()
		{
			var ingredientTypes = repo.GetIngredientTypes();
			var countList = new List<double>();
			var namesList = new List<string>();
			foreach (var t in ingredientTypes)
			{
				double count = 0;
				foreach (var i in t.Ingredients)
				{
					count++;

				}
				countList.Add(count);
				namesList.Add(t.Name);
			}
			typeDataArray = countList.ToArray();
			typeNamesArray = namesList.ToArray();

		}
	}
}
