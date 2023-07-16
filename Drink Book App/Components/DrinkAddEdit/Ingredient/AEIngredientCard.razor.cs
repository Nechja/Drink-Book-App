using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient
{
	public partial class AEIngredientCard
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		private bool FakeSubmit { get; set; } = false;
		private string ErrorText { get; set; } = string.Empty;

		private List<IngredientDisplayModel> Ingredients { get; set;} = new List<IngredientDisplayModel>();

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
			UpdateData();
		}

		protected private void UpdateData()
		{
			Ingredients.Clear();
			var data = repo.GetAllIngredient();
			foreach (var ingredient in data)
			{
				Ingredients.Add(new IngredientDisplayModel(ingredient));
			}
		}

		protected void KeyStop(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter") FakeSubmit = true;
		}

		void HandleValidSubmit()
		{
			if (FakeSubmit) 
			{
				FakeSubmit = false;
				return;
			}
			try
			{
				ErrorText = string.Empty;
				if (Ingredient.Id == 0)
				{
					repo.AddIngredient(Ingredient.DataModel);
				}
				else
				{
					repo.UpdateIngredient(Ingredient.DataModel);
				}
				Ingredient = new IngredientDisplayModel();
				UpdateData();
			}
			catch (Exception ex) 
			{
				ErrorText = ex.Message;
			}

		}

		void HandleInvalidSubmit()
		{
			if (FakeSubmit)
			{
				FakeSubmit = false;
				return;
			}
			ErrorText = string.Empty;
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

		protected void OnEdit(IngredientDisplayModel m) { Ingredient = m; }


		Func<IngredientDisplayModel, object> _groupBy = x =>
		{

			return x.IngredientType.Name;
		};


	}
}
