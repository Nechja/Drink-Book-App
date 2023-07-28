using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace DataAccess.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class DrinkDataModel : Logged, IDrinkDataModel, ISoftDelete
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }



		public bool Verification { get; set; }

		public IceDataModel? Ice { get; set; }
		public List<GarnishDataModel>? Garnishes { get; set; } = new();

		public RimDataModel? Rim { get; set; }
		public string? Notes { get; set; }

		public DrinkDataModel? Mod { get; set; }

		public GlassDataModel? Glass { get; set; }
		public Uri? Image { get; set; }

		public Uri? Link { get; set; }

		public List<DrinkTagDataModel>? Tags { get; set; } = new();
		public List<InstructionDataModel> Instructions { get; set; } = new();

		public List<DrinkBadgeDataModel>? Badges { get; set; } = new();

        public List<UserDrinkListsDataModel>? DrinkLists { get; set; } = new();

        public ViewsDataModel ViewsData { get; set; } = new();
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
        public bool? FINALDELETE { get; set; }

        public DrinkDataModel() { }


	}
}
