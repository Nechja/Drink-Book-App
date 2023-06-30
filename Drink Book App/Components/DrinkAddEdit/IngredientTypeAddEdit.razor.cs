using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit
{
    public partial class IngredientTypeAddEdit
    {
        [Inject]
        public DrinkRepository repo { get; set; }


        private List<IngredientTypeDisplayModel> _types { get; set; } = new();

        protected override void OnInitialized()
        {
        }


    }
}
