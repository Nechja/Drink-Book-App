using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models
{
    public class InstructionDisplayModel : IInstructionDataModel
    {
        public int Id { get; set; }

		[Range(0, float.MaxValue, ErrorMessage = "Please enter valid Number")]
		[RegularExpression(@"^\d+(\.\d{1,2})?$")]
		public float? Oz { get; set; }

		[Range(0, 5, ErrorMessage = "Please enter 1-5")]
		public int? DisplayWeight { get; set; }

		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string? Special { get; set; }
        [Required]
        public IngredientDisplayModel Ingredient { get; set; } = new IngredientDisplayModel();

		public FlagDisplayModel? Flag { get; set; } = new FlagDisplayModel();

		public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

        public InstructionDisplayModel() { }

        public InstructionDisplayModel(IInstructionDataModel model) 
        {
            Id = model.Id;
            Oz = model.Oz;
            Special = model.Special;
            DisplayWeight = model.DisplayWeight;

            if(model is InstructionDataModel)
            {
                InstructionDataModel m = (InstructionDataModel)model;
                Ingredient = new IngredientDisplayModel(m.Ingredient);
                if(m.Flag.Name != null)
                {
                    Flag = new FlagDisplayModel(m.Flag);
                }
                
            }
        }

        public string OzString
        {
            get
            {
                return $"{Oz.ToString()}ᵒᶻ";
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

		public InstructionDataModel DataModel
        {
            get
            {
                return new InstructionDataModel {
                    Id = this.Id,
                    Oz = this.Oz,
                    Special = this.Special,
                    Ingredient = this.Ingredient.DataModel,
                    DisplayWeight = this.DisplayWeight,
                    Flag = Flag.ToDataModel()
		        };
			}
        }


	}
}
