using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using System.Diagnostics;

namespace Drink_Book_App.Pages
{
	public partial class TagsDashboard
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

		protected override void OnInitialized()
		{
			var tags = repo.GetDrinkTags();
			foreach(var tag in tags) 
			{
				Tags.Add(new TagDisplayModel(tag));
			}
		}
	}
}
