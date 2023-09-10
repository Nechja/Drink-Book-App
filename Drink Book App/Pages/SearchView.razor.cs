using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using FuzzySharp;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;

using Xunit.Sdk;

namespace Drink_Book_App.Pages;

public partial class SearchView
{
	[Inject]
	public DrinkRepositoryAsync repo { get; set; }
	[Inject]
	NavigationManager navi { get; set; }
	private List<DrinkDisplayModel> drinks { get; set; } = new();
	private List<DrinkDisplayModel> filteredDrinks { get; set; } = new();

	private string    _ingredientSearch = string.Empty;
	private string _notingredientSearch = string.Empty;
	private string _tags = string.Empty;
	private string _method = string.Empty;


	protected override async Task OnInitializedAsync()
	{
		await UpdateData();
	}

	protected private async Task UpdateData()
	{
		drinks.Clear();

		var drinkdata = await repo.GetDrinks();
		foreach (DrinkDataModel d in drinkdata)
		{
			DrinkDisplayModel ddm = new DrinkDisplayModel();
			ddm.fromDrinkData(d);
			drinks.Add(ddm);
		}




	}

	public void NavTo(string Name, int Id)
	{
		navi.NavigateTo($"/Drink/{Name.Replace("#", String.Empty)}/{Id}");
	}

	private async Task DrinkFilter()
	{
        var searchTerms = _ingredientSearch.ToLower().Split(',');
		var notSearchTerms = _notingredientSearch.ToLower().Split(',');
		var tagTerms = _tags.ToLower().Split(',');
		var filtered = drinks.ToList();
		if (!_ingredientSearch.IsNullOrEmpty())
		{
            foreach (var term in searchTerms)
            {
                filtered = filtered.Where(x => x.Instructions.Any(y =>
                    y.Ingredient.Name.ToLower().Contains(term.Trim()) ||
                    (y.Ingredient.IngredientType != null && y.Ingredient.IngredientType.Name.ToLower().Contains(term.Trim()))
                )).ToList();
            }
        }

		if(!_notingredientSearch.IsNullOrEmpty())
		{
            foreach (var term in notSearchTerms)
            {
                filtered = filtered.Where(x => !x.Instructions.Any(y =>
                                y.Ingredient.Name.ToLower().Contains(term.Trim()) ||
                                (y.Ingredient.IngredientType != null && y.Ingredient.IngredientType.Name.ToLower().Contains(term.Trim()))
                                )).ToList();
            }
        }

		if(!tagTerms.IsNullOrEmpty())
		{
            foreach (var tag in tagTerms)
            {
                filtered = filtered.Where(x => x.Tags.Any(y => y.Value.ToLower().Contains(tag.Trim()))).ToList();
            }
        }

		if (!_method.IsNullOrEmpty())
		{
            filtered = filtered.Where(x => x.Instructions.Any(y =>
                y.Flag != null &&
                y.Flag.Name != null &&
                Fuzz.PartialRatio(y.Flag.Name.ToLower(), _method.ToLower().Trim()) > 60
            )).ToList();
        }


        filteredDrinks = filtered;
    }



}
