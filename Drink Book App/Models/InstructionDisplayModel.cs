using DataAccess.Models;
using DataAccess.Models.Interfaces;
using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static MudBlazor.Colors;

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
                if(m.Flag != null)
                {
                    Flag = new FlagDisplayModel(m.Flag);
                }
                
            }
        }

        public string OzString
        {
            get
            {
                
                if (Oz % 1 == 0)
                {
                    return $"{Oz.ToString()} ᵒᶻ";
                }

                float value = Oz.Value;
                float tolerance = 0.01f; // Adjust this tolerance based on your preference

                for (int denominator = 2; denominator <= 100; denominator++)
                {
                    int numerator = (int)Math.Round(value * denominator);

                    float fractionValue = (float)numerator / denominator;
                    if (Math.Abs(fractionValue - value) < tolerance)
                    {
                        if (numerator == 0)
                            return "0";
                        if (numerator == denominator)
                            return "1";
                        return $"{numerator}/{denominator} ᵒᶻ";
                    }
                }


                // If no exact or close match was found, return an approximation
                return $"{Oz.ToString()} ᵒᶻ";
            }
        }

        public string SpecialString
        {
            get
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                return textInfo.ToTitleCase(Special);
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
