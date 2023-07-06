using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata;
using MudBlazor;

namespace Drink_Book_App.Components.DrinkAddEdit.Instruction
{
    public partial class AEInstruction
    {
        [Inject]
        public DrinkRepository repo { get; set; }

        [CascadingParameter]
        public InstructionDisplayModel Model { get; set; } = new InstructionDisplayModel();

        [Parameter]
        public EventCallback<InstructionDisplayModel> OnSelectInstruction { get; set; }

		private List<FlagDisplayModel> ShakerFlags { get; set; } = new List<FlagDisplayModel>();
		private List<FlagDisplayModel> ServingFlags { get; set; } = new List<FlagDisplayModel>();

		private bool FakeSubmit { get; set; } = false;

        string ErrorText { get; set; } = string.Empty;

		private MudChip shakerSelected;
		private MudChip ShakerSelected
		{
			get { return shakerSelected; }
			set
			{
				shakerSelected = value;
				if (shakerSelected != null)
				{
					ShakerChipChanged(value);
				}
			}
		}

		private MudChip servingSelected;

		private MudChip ServingSelected 
		{ 
			get { return servingSelected; }
			set
			{
				servingSelected = value;
				if (servingSelected != null)
				{
					ServingChipChanged(value);
				}
			}
		}
		private bool ShakeOrServe { get; set; } = true;

		protected override void OnInitialized()
		{
			var seflags = repo.GetServingFlags();
			var shflags = repo.GetShakerFlags();
			foreach(var flag in seflags)
			{
				ServingFlags.Add(new FlagDisplayModel(flag));
			}
			foreach(var flag in shflags)
			{
				ShakerFlags.Add(new FlagDisplayModel(flag));
			}
		}

		protected void KeyStop(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter") FakeSubmit = true;
		}


		protected void OnSelectIngredientChange(IngredientDisplayModel m)
        {
            Model.Ingredient = m;
        }

        protected async Task OnValidSubmit()
        {
            if (FakeSubmit) 
            { 
                FakeSubmit = false;
                return; 
            }
            if (Model.Ingredient.Name == null)
            {
                ErrorText = "Ingredient Required";
                return;
            }
            await OnSelectInstruction.InvokeAsync(Model);
            Model = new InstructionDisplayModel();
		}

        private void OnSelectTag(List<TagDisplayModel> tags)
        {
            Model.Tags = tags;
        }

		protected void ShakerChipChanged(MudChip chip)
		{
			
		}

		protected void  ServingChipChanged(MudChip chip)
		{

		}
	}
}
