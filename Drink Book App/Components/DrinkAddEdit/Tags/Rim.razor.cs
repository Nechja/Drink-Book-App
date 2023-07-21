﻿using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Azure.SignalR.Common;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
	public partial class Rim
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
					await repo.AddRim(Model.RimDataModel);
				}
				else {await repo.UpdateRim(Model.RimDataModel); }

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
			StateHasChanged();

		}

		public void EditType(TagDisplayModel m)
		{
			Model = m;
		}

		public async Task DeleteType(TagDisplayModel m) 
		{
			await repo.DeleteRim(m.Id);
			await getUpdates();
			StateHasChanged();
		}

		private async Task getUpdates()
		{
			var typesdata = await repo.GetRimTypes();
			Types.Clear();
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

