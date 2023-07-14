
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;

namespace DataAccess.Models
{
	public class DrinkTagDataModel : Logged, ITagDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }

		public List<DrinkDataModel> Drinks { get; set; } = new();
		public DrinkTagDataModel() { }


		public DrinkTagDataModel(DrinkTagDataModel m) 
		{ 
			Id = m.Id;
			Value = m.Value;
		}

		public DrinkTagDataModel(string value)
		{
			Value = value;
		}
	}
}
