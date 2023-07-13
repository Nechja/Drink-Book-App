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

		private List<DrinkDisplayModel> softdeldrinks { get; set; } = new();

		protected override void OnInitialized()
		{
			UpdateData();
		}

		protected private void UpdateData()
		{
			var drinkdata = repo.GetDrinks();
			foreach (DrinkDataModel d in drinkdata)
			{
				DrinkDisplayModel ddm = new DrinkDisplayModel();
				ddm.fromDrinkData(d);
				drinks.Add(ddm);
			}

			var softdata = repo.GetAllDrinks();
			foreach (DrinkDataModel d in softdata)
			{
				if(d.IsDeleted)
				{
					DrinkDisplayModel ddm = new DrinkDisplayModel();
					ddm.fromDrinkData(d);
					softdeldrinks.Add(ddm);
				}

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

		public void DeleteDrink(int id)
		{
			repo.DeleteDrink(id);
		}

		private void UndoDel(int id)
		{
			repo.UndoSoftDeleteDrink(id);
		}
    }
}
