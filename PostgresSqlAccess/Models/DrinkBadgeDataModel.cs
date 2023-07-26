using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class DrinkBadgeDataModel
	{
		[Key] public int Id { get; set; }

		public string Icon { get; set; }
		public string HoverText { get; set; }

		public List<DrinkDataModel> Drinks { get; set; } = new();

		public DrinkBadgeDataModel() { }
	}
}
