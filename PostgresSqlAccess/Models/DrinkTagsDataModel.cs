using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class DrinkTagsDataModel
	{
		[ExplicitKey]
		public int id {  get; set; }
		public string Value { get; set; }
		public DrinkTagsDataModel() { }

		public DrinkTagsDataModel(string value) 
		{ 
			Value = value;
		}
	}
}
