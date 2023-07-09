using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Drink_Book_App.Components.DrinkAddEdit
{
    public partial class AEDrinkCard
    {
		[Inject]
		public DrinkRepository repo { get; set; }
		public DrinkDisplayModel Drink { get; set; } = new DrinkDisplayModel();
        public InstructionDisplayModel Instruction { get; set; } = new InstructionDisplayModel();
		public List<GlassDisplayModel> Glassware { get; set; } = new List<GlassDisplayModel>();

		bool FakeSubmit = false;

		string ErrorText { get; set; } = string.Empty;

		protected override void OnInitialized()
		{
			var glassdata = repo.GetGlassware();
			foreach (var glass in glassdata) 
			{
				Glassware.Add(new GlassDisplayModel(glass));
			}
		}

		protected void OnValidSubmit()
		{

			ErrorText = string.Empty;
			if (Drink.Instructions.Count < 2)
			{
				ErrorText = "Must have 2 or more Instructions";
				return;
			}
			if (FakeSubmit)
			{
				FakeSubmit = false;
				return;
			}
			if (Drink.Id == 0)
			{
				repo.AddDrink(Drink.GetDataModel());
			}
		}

		protected void OnSelectInstructionChange(InstructionDisplayModel m)
        {
            Drink.Instructions.Add(m);
            Instruction = new InstructionDisplayModel();
        }

		private void OnSelectTag(List<TagDisplayModel> tags)
		{
			Drink.Tags = tags;
		}
		private void OnEnterStop(bool s)
		{
			FakeSubmit = s;
		}

		protected void KeyStop(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter") FakeSubmit = true;
		}
	}
}
