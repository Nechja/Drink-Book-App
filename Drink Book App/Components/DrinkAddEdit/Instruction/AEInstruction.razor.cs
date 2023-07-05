using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

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


        protected void OnSelectIngredientChange(IngredientDisplayModel m)
        {
            Model.Ingredient = m;
        }
    }
}
