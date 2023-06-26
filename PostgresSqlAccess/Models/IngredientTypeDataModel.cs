using System.ComponentModel.DataAnnotations;
using DataAccess.Models.Interfaces;

namespace DataAccess.Models
{
    public class IngredientTypeDataModel : IIngredientTypeDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<IngredientDataModel> Ingredients { get; set; } = new();
	}
}
