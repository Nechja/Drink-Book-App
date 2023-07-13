using DataAccess.Models;
using DataAccess.Models.Interfaces;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using static MudBlazor.CategoryTypes;

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

		private string searchString { get; set; }


        public List<string> values { get; set; } = new List<string>();

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
                values.Add(d.Name);
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

        public void EditDrink(DrinkDataModel drink)
        {
            navi.NavigateTo($"/drinktools/editdrink/{drink.Id}");
        }

        public void DeleteDrink(int id)
		{
			repo.DeleteDrink(id);
            UpdateData();
        }

		private void UndoDel(int id)
		{
			repo.UndoSoftDeleteDrink(id);
            UpdateData();
        }

        private async Task<IEnumerable<string>> TagSearch(string value)
        {
            await Task.Delay(5);
            if (string.IsNullOrEmpty(value)) return new string[0];
            return values.Distinct().Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private Func<DrinkDisplayModel, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;

            if (x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;


            return false;
        };
    }
}
