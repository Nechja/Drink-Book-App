using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;

namespace DataAccess.Models
{
	public class IngredientTagDataModel : ITagDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }
		public List<IngredientDataModel> Ingredients { get; set; } = new();
	}
}
