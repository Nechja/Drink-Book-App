


using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Drink_Book_App.Models;


public class DrinkDisplayModel : IDrinkDataModel
{
	public TagDisplayModel? Garnish { get; set; }
	public TagDisplayModel? Ice { get; set; }
	public int Id { get; set; }
	public Uri? Image { get; set; }
	[Required]
	[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
	public string Name { get; set; }
	public string? Notes { get; set; }
	[Required]
	public GlassDisplayModel Glass { get; set; }

	public List<InstructionDisplayModel> Instructions { get; set; } = new List<InstructionDisplayModel>();

	public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

	public void fromDrinkData(DrinkDataModel drinkData)
	{
		this.Name = drinkData.Name;
		this.Notes = drinkData.Notes;
		this.Id = drinkData.Id;
		this.Image = drinkData.Image;
		this.Ice = new TagDisplayModel(drinkData.Ice);
		this.Garnish = new TagDisplayModel(drinkData.Garnish);
		this.Glass = new GlassDisplayModel(drinkData.Glass);
		foreach(DrinkTagDataModel t in drinkData.Tags)
		{
			Tags.Add(new TagDisplayModel(t));
		}
	}


	public DrinkDataModel GetDataModel()
	{
		DrinkDataModel drink = new DrinkDataModel();
		drink.Name = this.Name;
		drink.Notes = this.Notes;
	    drink.Id = this.Id;
		drink.Image = this.Image;
		drink.Ice = this.Ice.IceDataModel;
		drink.Garnish = this.Garnish.GarnishDataModel;
		drink.Glass = this.Glass.DataModel;
	    drink.Instructions = ParseInstructions();
		foreach(TagDisplayModel t in Tags)
		{
			drink.Tags.Add(t.DrinkTagDataModel);
		}
		return drink;
	}

	protected List<InstructionDataModel> ParseInstructions()
	{
		List<InstructionDataModel> instructions = new List<InstructionDataModel>();
		foreach (InstructionDisplayModel model in Instructions)
		{
			InstructionDataModel data = new InstructionDataModel(model.DataModel);
			foreach(TagDisplayModel tag in model.Tags)
			{
				data.Tags.Add(tag.InstructionTagDataModel);

			}
			instructions.Add(data);
		}
		return instructions;
	}

	public string DrinkTitleLong
	{ 
		get 
		{
			if(String.IsNullOrEmpty(this.Name)) return string.Empty;
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			string title = textInfo.ToTitleCase(Name);
			string servingstyle;
			string garnish;
			return title;
		}
	}
}
