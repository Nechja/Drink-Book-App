using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

using Microsoft.AspNetCore.Components.Web;


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


		private bool FakeSubmit { get; set; } = false;

        string ErrorText { get; set; } = string.Empty;

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
	}
}
