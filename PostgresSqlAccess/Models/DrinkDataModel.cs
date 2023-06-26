using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
	public class DrinkDataModel : IDrinkDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Ice { get; set; }
		public string? Garnish { get; set; }
		public string? Notes { get; set; }

		public DrinkDataModel? Mod { get; set; }
		public Uri? Image { get; set; }

		public List<DrinkTagDataModel>? Tags { get; set; } = new();
		public List<InstructionDataModel> Instructions { get; set; } = new();


		public DrinkDataModel() { }
	}
}
