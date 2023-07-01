using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient_Type
{
    public partial class  AEIngredientTypeCard
    {
        [Inject]
        public DrinkRepository repo { get; set; }
        public IngredientTypeDisplayModel IngredientDisplay { get; set; } = new IngredientTypeDisplayModel();
		public List<IngredientTypeDisplayModel> ingredientTypes { get; set; } = new List<IngredientTypeDisplayModel>();

		protected override Task OnInitializedAsync()
		{
			getUpdates();
			return base.OnInitializedAsync();
		}

		public void ValidSubmit()
        {
            repo.AddIngredientType(IngredientDisplay.DataModel);
			IngredientDisplay = new IngredientTypeDisplayModel();
			getUpdates();
		}

		private void getUpdates()
		{
			var typesdata = repo.GetIngredientTypes();
			foreach (IngredientTypeDataModel dataModel in typesdata)
			{
				var toadd = new IngredientTypeDisplayModel(dataModel);
				if (ingredientTypes.FirstOrDefault(i => i.Id == dataModel.Id) is null)
				{
					ingredientTypes.Add(toadd);
				}
				
			}
		}
    }
}
