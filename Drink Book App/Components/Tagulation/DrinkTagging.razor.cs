using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.Tagulation
{
	public partial class DrinkTagging
	{
		[Inject]
		public DrinkRepositoryAsync repo { get; set; }
		public List<DrinkTagType> Types { get; set; } = new List<DrinkTagType>();

		public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

		public TagDisplayModel Model { get; set; } = new TagDisplayModel();

		protected override async Task OnInitializedAsync()
		{
			GetData();
		}

		private async Task GetData()
		{
			Types.Clear();
			Tags.Clear();
			var tags = await repo.GetDrinkTags();
			foreach(var tag in tags)
			{
				Tags.Add(new TagDisplayModel(tag));
			}
			Types = await repo.GetDrinkTagTypes();
		}

		protected async Task OnEdit(TagDisplayModel m)
		{
			Model = m;
		}

		protected async Task OnDelete(TagDisplayModel m)
		{
			m.Deletable = false;


		}

		public async Task ValidSubmit()
		{

			try
			{
				if (Model.Id == 0)
				{
					await repo.AddDrinkTag(Model.DrinkTagDataModel);
				}
				else { await repo.UpdateDrinkTag(Model.DrinkTagDataModel); }

				Model = new TagDisplayModel();
				await GetData();
			}
			catch (Exception e)
			{

			}

		}



	}
}
