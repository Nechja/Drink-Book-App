using DataAccess.Models;
using DataAccess.Models.Interfaces;

namespace Drink_Book_App.Models
{
    public class InstructionDisplayModel : IInstructionDataModel
    {
        public int Id { get; set; }
        public int? Oz { get; set; }
        public string? Special { get; set; }

        public IngredientDisplayModel Ingredient { get; set; } = new IngredientDisplayModel();

        public InstructionDisplayModel() { }

        public InstructionDisplayModel(IInstructionDataModel model) 
        {
            Id = model.Id;
            Oz = model.Oz;
            Special = model.Special;

            if(model is InstructionDataModel)
            {
                var m = (InstructionDataModel)model;
                Ingredient = new IngredientDisplayModel(m.Ingredient);
            }
        }

        public string InstructionText
        {
            get 
            {
                string instruction = "";
                if(Oz != null)
                {
                    instruction += $"{Oz.ToString()}ᵒᶻ {Ingredient.Name}";
                    return instruction;
                }
                if(Special != null)
                {
                    instruction += $"{Special} {Ingredient.Name}";
                    return instruction;
                }
                
                return instruction;
            }
        }

    }
}
