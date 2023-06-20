using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	internal class DrinkDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Ice { get; set; }
		public string? Garnish { get; set; }
		public string? Notes { get; set; }
		public Uri? Image { get; set; }

		List<DrinkTagsDataModel> Tags { get; set; }
		List<DrinkIngredientDataModel> ingredients { get; set; }


		public DrinkDataModel() { }
	}
}
