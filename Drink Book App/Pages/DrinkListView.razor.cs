using DataAccess.Models;
using DataAccess.Models.Interfaces;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Pages
{
	public partial class DrinkListView
	{
		[Inject]
		NavigationManager navi { get; set; }
		[Inject]
		public DrinkRepository repo { get; set; }

		private List<DrinkDisplayModel> drinks { get; set; } = new();

		protected override void OnInitialized()
		{
			var drinkdata = repo.GetDrinks();
			foreach(DrinkDataModel d in drinkdata)
			{
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
    }
}
