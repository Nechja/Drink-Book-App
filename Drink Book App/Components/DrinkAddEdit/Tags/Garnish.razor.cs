using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
	public partial class Garnish
	{
		[Inject]
		public DrinkRepositoryAsync repo { get; set; }

		private string? errorValid;
		private string? showInvalid;
		public TagDisplayModel Model { get; set; } = new TagDisplayModel();
		public List<TagDisplayModel> Types { get; set; } = new List<TagDisplayModel>();


		protected override async Task OnInitializedAsync()
		{
			await getUpdates();
		}

		public async Task ValidSubmit()
		{
			errorValid = showInvalid = null;
			try
			{
				if (Model.Id == 0)
				{
					await repo.AddGarnish(Model.GarnishDataModel);
				}
				else {await repo.UpdateGarnish(Model.GarnishDataModel); }

				Model = new TagDisplayModel();
				await getUpdates();
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

		public void EditType(TagDisplayModel m)
		{
			Model = m;
		}

		public async Task DeleteType(TagDisplayModel m)
		{
			await repo.DeleteGarnish(m.Id);
			await getUpdates();
			StateHasChanged();
		}

		private async Task getUpdates()
		{
			var typesdata = await repo.GetGarnishTypes();
			Types.Clear();
			foreach (GarnishDataModel dataModel in typesdata)
			{
				var toadd = new TagDisplayModel(dataModel);
				if (Types.FirstOrDefault(i => i.Id == dataModel.Id) is null)
				{
					Types.Add(toadd);
				}

			}
		}
	}
}
