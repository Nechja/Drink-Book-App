using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Metadata;
using MudBlazor;
using static MudBlazor.CategoryTypes;

namespace Drink_Book_App.Components.DrinkAddEdit.Instruction
{
    public partial class AEInstruction
    {
        [Inject]
        public DrinkRepositoryAsync repo { get; set; }

        [CascadingParameter]
        public InstructionDisplayModel Model { get; set; } = new InstructionDisplayModel();

        [Parameter]
        public EventCallback<InstructionDisplayModel> OnSelectInstruction { get; set; }

		private List<FlagDisplayModel> Flags { get; set; } = new List<FlagDisplayModel>();
		private bool FakeSubmit { get; set; }

        string ErrorText { get; set; } = string.Empty;

		private bool OpenIngredient;



		private MudChip chipSelected;

		private MudChip ChipSelected 
		{ 
			get { return chipSelected; }
			set
			{
				chipSelected = value;
				ChipChanged(value);
			}
		}

		protected override async Task OnInitializedAsync()
		{
			var flagdata = await repo.GetFlags();
			foreach(var flag in flagdata)
			{
				Flags.Add(new FlagDisplayModel(flag));
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
		private void OnEnterStop(bool s)
		{
			FakeSubmit = s;
		}

		protected void ChipChanged(MudChip chip)
		{
			Model.Flag = (FlagDisplayModel)chip.Value;
		}

		protected void SelectedFlag(FlagDisplayModel f)
		{
			Model.Flag = f;
		}

		protected void OnEdit(IngredientDisplayModel m)
		{

		}
	}
}
