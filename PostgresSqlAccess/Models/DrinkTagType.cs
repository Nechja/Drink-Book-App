using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class DrinkTagType
	{
		[Key] public int Id { get; set; }
		public string Type { get; set; }


		public string Color { get; set; }

		public List<DrinkTagDataModel> DrinkTags { get; set; } = new();
	}
}
