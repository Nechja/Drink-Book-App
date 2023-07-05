using Drink_Book_App.Models;

namespace Drink_Book_App.Components.DrinkAddEdit
{
    public partial class AEDrinkCard
    {
        public DrinkDisplayModel Drink { get; set; } = new DrinkDisplayModel();
        public InstructionDisplayModel Instruction { get; set; } = new InstructionDisplayModel();



        protected void OnSelectInstructionChange(InstructionDisplayModel m)
        {
            Drink.Instructions.Add(m);
            Instruction = new InstructionDisplayModel();
        }
    }
}
