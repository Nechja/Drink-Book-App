


using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.Globalization;

namespace Drink_Book_App.Models;


public class DrinkDisplayModel : IDrinkDataModel
{
	public string? Garnish { get; set; }
	public string? Ice { get; set; }
	public int Id { get; set; }
	public Uri? Image { get; set; }
	public string Name { get; set; }
	public string? Notes { get; set; }

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
			
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			string title = textInfo.ToTitleCase(Name);
			return title; 
		}
	}
}
