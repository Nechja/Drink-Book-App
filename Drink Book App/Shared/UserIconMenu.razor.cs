using DataAccess.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using DataAccess.Models;

namespace Drink_Book_App.Shared
{
    public partial class UserIconMenu
    {
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        [Inject]
        public DrinkRepositoryAsync repo { get; set; }

        public List<UserDrinkListsDataModel> drinkLists { get; set; } = new List<UserDrinkListsDataModel>();


        bool open;

        private string Username = "";
        private string Picture = "";

        private void onAvatarClick()
        {
            open = !open;
        }

        private async Task onAddClick()
        {
            if(Username == "") { return; }
            await repo.NewList(Username);
            drinkLists = await repo.GetLists(Username);
        }

        protected override async Task OnInitializedAsync()
        {
            if (authenticationState is not null)
            {
                var state = await authenticationState;

                Username = state?.User?.Identity?.Name ?? string.Empty;

                Picture = state?.User.Claims
                            .Where(c => c.Type.Equals("picture"))
                            .Select(c => c.Value)
                            .FirstOrDefault() ?? string.Empty;


                if (Username != String.Empty)
                {
                    await repo.MakeUserLink(Username);
                    drinkLists = await repo.GetLists(Username);
                }

            }
            await base.OnInitializedAsync();
        }
    }
}
