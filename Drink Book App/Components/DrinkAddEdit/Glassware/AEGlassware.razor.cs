using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Glassware
{
	public partial class AEGlassware
	{
		[Inject]
		public DrinkRepository repo { get; set; }

		private string? errorValid;
		private string? showInvalid;

		public List<GlassDisplayModel> Glassware { get; set;} = new List<GlassDisplayModel>();
		public GlassDisplayModel Glass { get; set; } = new GlassDisplayModel();


		protected private void OnValidSubmit()
		{
			errorValid = null;
			try
			{
				if (Glass.Id == 0)
				{
					repo.AddGlass(Glass.DataModel);
				}
				else
				{
					repo.UpdateGlass(Glass.DataModel);
				}
				Glass = new GlassDisplayModel();
			}
			catch (Exception ex)
			{
				errorValid = ex.Message;
			}
			UpdateData();
		}
		protected override void OnInitialized()
		{
			UpdateData();
		}

		protected void UpdateData()
		{
			Glassware.Clear();
			var data = repo.GetGlassware();
			foreach (var item in data)
			{
				Glassware.Add(new GlassDisplayModel(item));
			}
		}

		protected private void OnEdit(GlassDisplayModel model)
		{
			if (model == null) { return; }
			Glass = model;
		}

		protected private void OnDelete(GlassDisplayModel model)
		{
			if (model == null) { return; }
			repo.DeleteGlass(model.Id);
			UpdateData();
		}
	}
}
