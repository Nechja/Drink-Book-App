using DataAccess.Models;
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

		

		private string _ingredientTypeText = string.Empty;
		public string IngredientTypeText
		{
			get
			{
				return _ingredientTypeText;
			}
			set
			{
				_ingredientTypeText = value;
				StateHasChanged();
			}
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		void HandleValidSubmit()
		{
			return;
		}

		void HandleInvalidSubmit()
		{
			return;
		}

		protected void OnSelectIngredientChange(IngredientTypeDisplayModel m)
		{
			 Ingredient.IngredientType = m;
		}

		protected void OnSelectTagsChanged(List<TagDisplayModel> tags)
		{
			Ingredient.Tags = tags;
		}


	}
}
