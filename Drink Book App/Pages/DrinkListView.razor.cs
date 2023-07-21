using DataAccess.Models;
using DataAccess.Models.Interfaces;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Drink_Book_App.Pages
{
	public partial class DrinkListView
	{
		[Inject]
		NavigationManager navi { get; set; }
		[Inject]
		public DrinkRepositoryAsync repo { get; set; }

		private List<DrinkDisplayModel> drinks { get; set; } = new();

		private List<DrinkDisplayModel> softdeldrinks { get; set; } = new();

		private string searchString { get; set; }

        MudChip[] selected;


        protected override async Task OnInitializedAsync()
		{
			await UpdateData();
		}

		protected private async Task UpdateData()
		{
			drinks.Clear(); 
			softdeldrinks.Clear();
			searchString = string.Empty;

			var drinkdata = await repo.GetDrinks();
			foreach (DrinkDataModel d in drinkdata)
			{
                await Task.Delay(1000);
                DrinkDisplayModel ddm = new DrinkDisplayModel();
				ddm.fromDrinkData(d);
				drinks.Add(ddm);
            }


		}


		public void NavTo(string Name, int Id)
		{
			navi.NavigateTo($"/Drink/{Name}/{Id}");
		}

        public void EditDrink(string Name, int Id)
        {
            navi.NavigateTo($"/drinktools/editdrink/{Id}");
        }


        private Func<DrinkDisplayModel, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;

			if (x.Instructions.Any(i => i.Ingredient.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
                return true;

            if (x.Instructions.Any(i => i.Ingredient.IngredientType.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
                return true;

            return false;
        };
    }
}
