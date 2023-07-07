


using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Drink_Book_App.Models;


public class DrinkDisplayModel : IDrinkDataModel
{
	public string? Garnish { get; set; }
	public string? Ice { get; set; }
	public int Id { get; set; }
	public Uri? Image { get; set; }
	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
	public string Name { get; set; }
	public string? Notes { get; set; }
	[Required]
	public GlassDisplayModel Glass { get; set; }

	public List<InstructionDisplayModel> Instructions { get; set; } = new List<InstructionDisplayModel>();

	public void fromDrinkData(IDrinkDataModel drinkData)
	{
		this.Name = drinkData.Name;
		this.Notes = drinkData.Notes;
		this.Id = drinkData.Id;
		this.Image = drinkData.Image;
		this.Ice = drinkData.Ice;
		this.Garnish = drinkData.Garnish;
	}

	public string DrinkTitleLong
	{ 
		get 
		{
			if(String.IsNullOrEmpty(this.Name)) return string.Empty;
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			string title = textInfo.ToTitleCase(Name);
			return title; 
		}
	}
}
