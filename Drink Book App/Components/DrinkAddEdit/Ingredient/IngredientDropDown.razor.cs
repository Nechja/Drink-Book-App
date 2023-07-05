using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient
{
	public partial class IngredientDropDown
	{
		[Inject]
		public DrinkRepository repo { get; set; }

        [Parameter]
        public EventCallback<IngredientDisplayModel> OnSelectIngredient { get; set; }

		[CascadingParameter]
		public IngredientDisplayModel Model { get; set; } = new IngredientDisplayModel();



		private List<IngredientDisplayModel> ingredient { get; set; } = new List<IngredientDisplayModel>();
		public List<String> IngredientNames = new List<String>();


		private string searchText = string.Empty;

		private string SearchText
		{
			get { return searchText; }
			set
			{
				searchText = value;
				StateHasChanged();
				if (ingredient.FirstOrDefault(x => x.Name == value) is null)
				{
					return;
				}
				IngredientDisplayModel mod = ingredient.FirstOrDefault(x => x.Name == value);
				EventCallbackIngredient(mod);
			}
		}

		protected override void OnInitialized()
		{
			var typesdata = repo.GetAllIngredient();
			foreach(IngredientDataModel dataModel in typesdata)
			{
				ingredient.Add(new IngredientDisplayModel(dataModel));
				IngredientNames.Add(dataModel.Name);


			}
			base.OnInitialized();
		}

		private async Task<IEnumerable<string>> Search(string value)
		{
			await Task.Delay(5);
			if (string.IsNullOrEmpty(value)) return new string[0];
			return IngredientNames.Distinct().Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
		}

		protected async Task EventCallbackIngredient(IngredientDisplayModel m)
		{
			OnSelectIngredient.InvokeAsync(m);
		}

		protected void Closed(MudChip chip)
		{
			SearchText = string.Empty;
			Model = new IngredientDisplayModel();
			StateHasChanged();
		}
	}
}
