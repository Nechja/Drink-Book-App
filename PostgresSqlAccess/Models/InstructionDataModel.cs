using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class InstructionDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }
		public int? Oz { get; set; }
		public string? Special { get; set; }
		public IngredientDataModel Ingredient { get; set; }

		public DrinkDataModel Drink { get; set; } = new();

		public List<InstructionTagDataModel> Tags { get; set; }

		public InstructionDataModel() { }
	}
}
