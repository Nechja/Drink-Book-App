using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Components;

namespace The_Drink_Book.Pages
{
	public partial class DrinkView
	{
		[Inject]
		public DrinkRepository repo { get; set; }
		[Parameter]
		public String? DrinkName { get; set; } = default;
		[Parameter]
		public int? DrinkId { get; set; }

		public DrinkDataModel Drink { get; set; }
		private bool failed = true;

		protected override void OnInitialized()
		{
			if(DrinkName  == null) { return; } 
			if(DrinkId == null) { return; }
			Drink = repo.GetDrinkById(DrinkId.Value);
			if (DrinkName.ToLower() == Drink.Name.ToLower())
			{
				failed = false;
			}
		}
	}
}
