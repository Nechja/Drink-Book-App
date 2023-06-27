using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Pages
{
	public partial class DrinkView
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		public DrinkDataModel Drink { get; set; }

		protected override void OnInitialized()
		{
			Drink = repo.GetDrinkById(4);
		}
	}
}
