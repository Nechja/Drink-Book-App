using System.ComponentModel.DataAnnotations;
using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
	[Index(nameof(Name), IsUnique = true)]
    public class IngredientTypeDataModel : IIngredientTypeDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public List<IngredientDataModel> Ingredients { get; set; } = new();
	}
}
