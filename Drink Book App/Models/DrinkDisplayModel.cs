﻿


using DataAccess.Models;
using DataAccess.Models.Interfaces;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Drink_Book_App.Models;


public class DrinkDisplayModel : DisplayDeleteProtection, IDrinkDataModel
{
	public List<TagDisplayModel>? Garnishes { get; set; } = new List<TagDisplayModel>();
	public TagDisplayModel? Ice { get; set; }

	public TagDisplayModel Rim { get; set; }
	public int Id { get; set; }

	public bool Verification { get; set; }

	public DrinkDataModel Mod { get; set; }

    public Uri? Image { get; set; }

	public Uri? Link { get; set; }
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
		this.Link = drinkData.Link;
		this.Verification = drinkData.Verification;
		this.Image = drinkData.Image;
		if(drinkData.Mod != null)
		{
            this.Mod = drinkData.Mod;
        }
		
		if(drinkData.Ice != null) this.Ice = new TagDisplayModel(drinkData.Ice);
		if(drinkData.Glass != null) this.Glass = new GlassDisplayModel(drinkData.Glass);
		if(drinkData.Rim != null) this.Rim = new TagDisplayModel(drinkData.Rim);

		if(drinkData.Garnishes.Any()) foreach (GarnishDataModel g in drinkData.Garnishes)
		{
			Garnishes.Add(new TagDisplayModel(g));
		}
		if (drinkData.Tags.Any()) foreach (DrinkTagDataModel t in drinkData.Tags)
		{
			Tags.Add(new TagDisplayModel(t));
		}
		foreach (var i in drinkData.Instructions)
		{
			Instructions.Add(new InstructionDisplayModel(i));
		}

	}


	public DrinkDataModel GetDataModel()
	{
		DrinkDataModel drink = new DrinkDataModel();
		drink.Name = this.Name;
		drink.Notes = this.Notes;
	    drink.Id = this.Id;
		drink.Image = this.Image;
		drink.Verification = this.Verification;
		drink.Link = this.Link;
        if (this.Mod != null) drink.Mod = this.Mod;
		if (Ice != null) drink.Ice = this.Ice.IceDataModel;
		if(Rim != null) drink.Rim = this.Rim.RimDataModel;
		foreach (TagDisplayModel t in Garnishes)
		{
			drink.Garnishes.Add(t.GarnishDataModel);
		}
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
			InstructionDataModel data = new InstructionDataModel(model.GetDataModel());
			foreach(TagDisplayModel tag in model.Tags)
			{
				data.Tags.Add(tag.InstructionTagDataModel);

			}
			instructions.Add(data);
		}
		return instructions;
	}

	public string DrinkEndLong
	{ 
		get 
		{
			if(String.IsNullOrEmpty(this.Name)) return string.Empty;
			TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
			string style;
			if (Ice != null)style = $"{textInfo.ToLower(Glass.Name)} {textInfo.ToLower(Ice.Value)}";
			else style = $"{Glass.Name}";
			if (Rim != null) style += $" w/ {textInfo.ToLower(Rim.Value)}";
			if (Garnishes.Any())
			{
				string garnishstring = " & Garnished w/ ";
				int g = Garnishes.Count();
				foreach(var garnish in Garnishes)
				{
					garnishstring += $" {garnish},";
				}
				style += garnishstring.TrimEnd(',');
            }
			return textInfo.ToLower(style);
		}
	}

    public string DrinkTitle
    {
        get
        {
            if (String.IsNullOrEmpty(this.Name)) return string.Empty;
            string pattern = @"\(([^)]*)\)";
            
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string title = textInfo.ToTitleCase(Name);
            MatchCollection matches = Regex.Matches(title, pattern);

			foreach (Match match in matches)
			{
				title = title.Replace(match.Value, string.Empty);
			}
            return title;
        }
    }

	public string DrinkVersion
	{
		get
		{
            if (String.IsNullOrEmpty(this.Name)) return string.Empty;
            string pattern = @"\(([^)]*)\)";

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            string title = textInfo.ToTitleCase(Name);
            MatchCollection matches = Regex.Matches(title, pattern);
			string version = string.Empty;
            foreach (Match match in matches)
            {
				version += match.Value;
            }
            return version;
        }
	}

	public Color VerifiedColor
	{
		get
		{
			if (Verification)
			{
				return Color.Success;
			}
			else
			{
				return Color.Error;
			}
		}
	}

	public string VerifiedIcon
	{
		get
		{
			if (Verification) { return Icons.Material.Filled.Verified; }
			else { return @Icons.Material.Filled.Pending; }


        }
	}

}
