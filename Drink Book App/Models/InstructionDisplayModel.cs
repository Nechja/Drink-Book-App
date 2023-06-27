using DataAccess.Models;
using DataAccess.Models.Interfaces;

namespace Drink_Book_App.Models
{
    public class InstructionDisplayModel : IInstructionDataModel
    {
        public int Id { get; set; }
        public int? Oz { get; set; }
        public string? Special { get; set; }

        public IngredientDisplayModel ingredient { get; set; }

        public InstructionDisplayModel() { }

        public InstructionDisplayModel(IInstructionDataModel model) 
        {
            Id = model.Id;
            Oz = model.Oz;
            Special = model.Special;

            if(model is InstructionDataModel)
            {
                var m = (InstructionDataModel)model;
                ingredient = new IngredientDisplayModel(m.Ingredient);
            }
        }

    }
}
