using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;

namespace Drink_Book_App.Components.DrinkAddEdit
{
    public partial class IngredientTypeAddEditCard
    {
        [Inject]
        public DrinkRepository repo { get; set; }


        [Parameter]
        public IngredientTypeDisplayModel? Ingredient { get; set; }

        public IngredientTypeDisplayModel _type { get; set; } = new IngredientTypeDisplayModel();

        private void HandleSubmit()
        {
            //repo.AddIngredientType(_type.DataModel);
        }

        private void HandleInvaldSubmit()
        {
            throw new NotImplementedException();
        }
    }
}
