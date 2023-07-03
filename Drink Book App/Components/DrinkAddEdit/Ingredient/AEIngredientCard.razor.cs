using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient
{
	public partial class AEIngredientCard
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		public IngredientDisplayModel Ingredient { get; set; } = new IngredientDisplayModel();

	}
}
