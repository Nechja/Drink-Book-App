using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class GarnishDrinkLinkModel
	{
		public int DrinkId { get; set; }
		public int GarnishID { get; set; }

		public DrinkDataModel Drink { get; set; }
		public GarnishDataModel Garnish { get; set; }
	}
}
