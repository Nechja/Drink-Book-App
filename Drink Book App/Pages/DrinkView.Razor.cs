using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Pages
{
	public partial class DrinkView
	{
		[Inject]
		public DrinkRepositoryAsync repo { get; set; }
		[Parameter]
		public String? DrinkName { get; set; } = default;
		[Parameter]
		public int? DrinkId { get; set; }

		public DrinkDataModel Drink { get; set; }
		private bool failed = true;

		protected override async Task OnInitializedAsync()
		{
			if(DrinkName  == null) { return; } 
			if(DrinkId == null) { return; }
			Drink = await repo.GetDrinkById(DrinkId.Value);
			if (DrinkName.ToLower() == Drink.Name.ToLower())
			{
				failed = false;
			}
		}
	}
}
