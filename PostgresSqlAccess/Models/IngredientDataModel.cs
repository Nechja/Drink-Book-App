using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	internal class IngredientDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }
		public string Name { get; set; }
		public IngredientTypeDataModel Type { get; set; }
		public int? Oz { get; set; }
		public string? Special { get; set; }

		public List<IngredientTagDataModel>? Tags { get; set; }
		

		public IngredientDataModel() { }

	}
}
