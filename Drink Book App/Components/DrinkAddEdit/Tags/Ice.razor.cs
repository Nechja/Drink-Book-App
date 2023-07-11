using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
	public partial class Ice
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		private string? errorValid;
		private string? showInvalid;
		public IngredientTypeDisplayModel IngredientDisplay { get; set; } = new IngredientTypeDisplayModel();
		public List<IngredientTypeDisplayModel> ingredientTypes { get; set; } = new List<IngredientTypeDisplayModel>();


		protected override Task OnInitializedAsync()
		{
			getUpdates();

			return base.OnInitializedAsync();
		}

		public void ValidSubmit()
		{
			errorValid = showInvalid = null;
			try
			{
				if (IngredientDisplay.Id == 0)
				{
					repo.AddIngredientType(IngredientDisplay.DataModel);
				}
				else { repo.UpdateIngredientType(IngredientDisplay.DataModel); }

				IngredientDisplay = new IngredientTypeDisplayModel();
				getUpdates();
			}
			catch (Exception e)
			{
				if (e is Microsoft.EntityFrameworkCore.DbUpdateException)
				{
					if (e.InnerException is Npgsql.PostgresException)
					{
						errorValid = e.InnerException.Message;
						showInvalid = "border-warning";
					}
				}
			}

		}

		public void EditType(IngredientTypeDisplayModel ingredientType)
		{
			IngredientDisplay = ingredientType;
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
}
