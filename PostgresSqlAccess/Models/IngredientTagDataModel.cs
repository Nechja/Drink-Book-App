using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DataAccess.Models
{
	[Index(nameof(Value), IsUnique = true)]
	public class IngredientTagDataModel : Logged, ITagDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }
		public string Mod { get; set; }
		public List<IngredientDataModel> Ingredients { get; set; } = new();

	}
}
