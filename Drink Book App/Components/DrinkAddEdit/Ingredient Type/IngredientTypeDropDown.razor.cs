﻿using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient_Type
{
	public partial class IngredientTypeDropDown
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		[CascadingParameter]
		public IngredientTypeDisplayModel Model { get; set; } = new IngredientTypeDisplayModel();

		private bool openIngredient;

		private bool OpenIngredient
		{
			get => openIngredient;
			set
			{
				openIngredient = value;
				if (!openIngredient)
				{
					GetData();
				}
			}
		}



		private string searchText = string.Empty;

		private string SearchText
		{
			get { return searchText; }
			set 
			{ 
				searchText = value;
				StateHasChanged();
				if (ingredientTypes.FirstOrDefault(x => x.Name == value) is null)
				{
					return;
				}
				IngredientTypeDisplayModel mod = ingredientTypes.FirstOrDefault(x => x.Name == value);
				EventCallbackIngredient(mod);
			}
		}
		[Parameter]
		public EventCallback<IngredientTypeDisplayModel> OnSelectIngredient { get; set; }
		private List<IngredientTypeDisplayModel> ingredientTypes { get; set; } = new List<IngredientTypeDisplayModel>();
		public List<String> IngredientTypeNames = new List<String>();

		protected override void OnInitialized()
		{
			GetData();
			base.OnInitialized();
		}

		protected private void GetData()
		{
			ingredientTypes.Clear();
			IngredientTypeNames.Clear();
			var typesdata = repo.GetIngredientTypes();
			foreach (IngredientTypeDataModel dataModel in typesdata)
			{
				ingredientTypes.Add(new IngredientTypeDisplayModel(dataModel));
				IngredientTypeNames.Add(dataModel.Name);


			}
		}

		private async Task<IEnumerable<string>> Search(string value)
		{
			await Task.Delay(5);
			if (string.IsNullOrEmpty(value)) return new string[0];
			return IngredientTypeNames.Distinct().Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
		}

		protected async Task EventCallbackIngredient(IngredientTypeDisplayModel m)
		{
			OnSelectIngredient.InvokeAsync(m);
		}

		protected void Closed(MudChip chip) 
		{
			SearchText = string.Empty;
			Model = new IngredientTypeDisplayModel();
			StateHasChanged();
		}
	}
}
