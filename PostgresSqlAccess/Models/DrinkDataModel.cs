using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class DrinkDataModel : IDrinkDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public IceDataModel? Ice { get; set; }
		public List<GarnishDataModel>? Garnishes { get; set; } = new();

		public RimDataModel? Rim { get; set; }
		public string? Notes { get; set; }

		public DateTime? Created { get; set; }

		public DrinkDataModel? Mod { get; set; }

		public GlassDataModel? Glass { get; set; }
		public Uri? Image { get; set; }

		public List<DrinkTagDataModel>? Tags { get; set; } = new();
		public List<InstructionDataModel> Instructions { get; set; } = new();


		public DrinkDataModel() { }


	}
}
