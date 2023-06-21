using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class IngredientTypeDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<IngredientDataModel> Ingredients { get; set; } = new();
	}
}
