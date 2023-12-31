﻿using DataAccess.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using DataAccess.Models;
using System.Net;

namespace Drink_Book_App.Shared
{
    public partial class UserIconMenu
    {
        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }
        [Inject]
        public DrinkRepositoryAsync repo { get; set; }

		[Inject]
		NavigationManager navi { get; set; }

		public List<UserDrinkListsDataModel> drinkLists { get; set; } = new List<UserDrinkListsDataModel>();


        bool open;

        private string Username = "";
        private string Picture = "";

        private async Task onAvatarClick()
        {
            open = !open;
            await GetData();
		}

        private async Task onAddClick()
        {
            if(Username == "") { return; }
            await repo.NewList(Username);
            drinkLists = await repo.GetLists(Username);

		}

        private async Task onListNav(string drinklistname, int listid)
        {
            string username = await repo.GetUserLink(Username);
            navi.NavigateTo($"/Lists/{username}/{drinklistname}/{listid}");
            open = false;
            StateHasChanged();
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
                }

            }
            await base.OnInitializedAsync();
        }

        private async Task GetData()
        {
            drinkLists.Clear();
			drinkLists = await repo.GetLists(Username);
		}
    }
}
