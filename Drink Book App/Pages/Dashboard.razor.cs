using DataAccess.Services;
using Drink_Book_App.Data;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Pages
{
	public partial class Dashboard
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		public List<(int count, string name)> IngredientTypesCounter = new List<(int count, string name)>();

		protected override void OnInitialized()
		{
			var ingredientTypes = repo.GetIngredientTypes();
			foreach (var t in ingredientTypes)
			{
				int count = 0;
				foreach (var i in t.Ingredients)
				{
					count++;
				}
				IngredientTypesCounter.Add((count, t.Name));
			}
		}
	}
}
