using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
	public partial class Rim
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		private string? errorValid;
		private string? showInvalid;
		public TagDisplayModel Model { get; set; } = new TagDisplayModel();
		public List<TagDisplayModel> Types { get; set; } = new List<TagDisplayModel>();


		protected override Task OnInitializedAsync()
		{
			getUpdates();

			return base.OnInitializedAsync();
		}

		public void ValidSubmit()
		{
			errorValid = showInvalid = null;
			try
			{
				if (Model.Id == 0)
				{
					repo.AddRim(Model.RimDataModel);
				}
				else { repo.UpdateRim(Model.RimDataModel); }

				Model = new TagDisplayModel();
				getUpdates();
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

		public void DeleteType(TagDisplayModel m) 
		{
			Model = m;
		}

		private void getUpdates()
		{
			var typesdata = repo.GetRimTypes();
			foreach (RimDataModel dataModel in typesdata)
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

