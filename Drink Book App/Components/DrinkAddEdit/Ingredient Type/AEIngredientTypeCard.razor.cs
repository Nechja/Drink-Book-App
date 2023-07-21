using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using static MudBlazor.CategoryTypes;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient_Type
{
	public partial class AEIngredientTypeCard
	{
		[Inject]
		public DrinkRepositoryAsync repo { get; set; }

		private string? errorValid;
		private string? showInvalid;
		public IngredientTypeDisplayModel IngredientDisplay { get; set; } = new IngredientTypeDisplayModel();
		public List<IngredientTypeDisplayModel> ingredientTypes { get; set; } = new List<IngredientTypeDisplayModel>();


		protected override async Task OnInitializedAsync()
		{
			await getUpdates();
		}

		public async Task ValidSubmit()
		{
			errorValid = showInvalid = null;
			try
			{
				if (IngredientDisplay.Id == 0)
				{
					await repo.AddIngredientType(IngredientDisplay.DataModel);
				}
				else { await repo.UpdateIngredientType(IngredientDisplay.DataModel); }

				IngredientDisplay = new IngredientTypeDisplayModel();
				await getUpdates();
			}
			catch (Exception e)
			{
				if(e is Microsoft.EntityFrameworkCore.DbUpdateException)
				{
					if(e.InnerException is Npgsql.PostgresException)
					{
						errorValid = e.InnerException.Message;
						showInvalid = "border-warning";
					}
				}
			}

		}

		public void OnEdit(IngredientTypeDisplayModel ingredientType)
		{
			IngredientDisplay = ingredientType;
		}

		private async Task getUpdates()
		{
			var typesdata = await repo.GetIngredientTypes();
			foreach (IngredientTypeDataModel dataModel in typesdata)
			{
				var toadd = new IngredientTypeDisplayModel(dataModel);
				if (ingredientTypes.FirstOrDefault(i => i.Id == dataModel.Id) is null)
				{
					ingredientTypes.Add(toadd);
				}
				
			}
		}

		protected async Task DeleteType(int id)
		{
			try
			{
				await repo.DeleteIngredientType(id);
			}
			catch (Exception e)
			{
				errorValid = e.Message;
			}
			
		}



	}
}
