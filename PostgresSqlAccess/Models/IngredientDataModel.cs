using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	 public class IngredientDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }

		public string Name { get; set; }

		public IngredientTypeDataModel IngredientType { get; set; } = new();

		

		public List<InstructionDataModel> Instructions { get; set; } = new();
		public List<IngredientTagDataModel> Tags { get; } = new();
	}
}
