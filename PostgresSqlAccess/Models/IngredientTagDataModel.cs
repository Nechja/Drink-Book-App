using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	internal class IngredientTagDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }
		public string Value { get; set; }
		public IngredientTagDataModel() { }

		public IngredientTagDataModel(string value)
		{ 
			Value = value;
		}
	}
}
