using DataAccess.Services;
using Microsoft.AspNetCore.Components;
using static MudBlazor.CategoryTypes;

namespace Drink_Book_App.Shared
{
    public partial class NavMenu
    {
        [Inject]
        NavigationManager navi { get; set; }
        [Inject]
        public DrinkRepository repo { get; set; }


        bool open = false;

        protected override void OnInitialized()
        {
            var drinklist = repo.GetDrinks();
            foreach (var drink in drinklist)
            {
                
            }
        }


        void ToggleDrawer()
        {
            open = !open;
        }


    }
}
