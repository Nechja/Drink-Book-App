using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models
{
    public class InstructionDisplayModel : IInstructionDataModel
    {
        public int Id { get; set; }


		[Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
		public int? Oz { get; set; }
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string? Special { get; set; }
        [Required]
        public IngredientDisplayModel Ingredient { get; set; } = new IngredientDisplayModel();

        public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

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
