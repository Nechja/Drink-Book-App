using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit.Ingredient_Type
{
    public partial class  AEIngredientTypeCard
    {
        [Inject]
        public DrinkRepository repo { get; set; }
        public IngredientTypeDisplayModel IngredientDisplay { get; set; } = new IngredientTypeDisplayModel();

        public void ValidSubmit()
        {
            repo.AddIngredientType(IngredientDisplay.DataModel);
        }
    }
}
