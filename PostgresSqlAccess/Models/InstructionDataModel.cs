using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;

namespace DataAccess.Models
{
    public class InstructionDataModel : IInstructionDataModel
	{
		[Key]
		public int Id { get; set; }
		public float? Oz { get; set; }
		public string? Special { get; set; }
		public int? DisplayWeight { get; set; }

		public FlagDataModel? Flag { get; set; } = new();
		public IngredientDataModel Ingredient { get; set; } = new();

		public DrinkDataModel Drink { get; set; } = new();

		public List<InstructionTagDataModel>? Tags { get; set; } = new();

		public DateTime? Created { get; set; }

		public InstructionDataModel(InstructionDataModel dataModel) 
		{
			Id = dataModel.Id;
			Oz = dataModel.Oz;
			Special = dataModel.Special;
			Ingredient = dataModel.Ingredient;
			Flag = dataModel.Flag;
			
		}

		public InstructionDataModel() { }
	}
}
