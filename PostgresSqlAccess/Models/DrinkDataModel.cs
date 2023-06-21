using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public class DrinkDataModel
	{
		[ExplicitKey]
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Ice { get; set; }
		public string? Garnish { get; set; }
		public string? Notes { get; set; }
		public Uri? Image { get; set; }

		public List<DrinkTagsDataModel> Tags { get; set; }  = new();
		public List<InstructionDataModel> Instructions { get; set; } = new();


		public DrinkDataModel() { }
	}
}
