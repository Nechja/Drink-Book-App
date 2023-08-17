using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Components;
using Drink_Book_App.Components.DrinkList;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Runtime.CompilerServices;
using static MudBlazor.Colors;

namespace Drink_Book_App.Pages
{
	public partial class UserDrinkListView
	{

		[Parameter]
		public string UserDisplayName { get; set; }
		[Parameter]
		public string ListName { get; set; }

		[Parameter]
		public int ListId { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }

        [Inject]
		public DrinkRepositoryAsync repo { get; set; }

        [Inject]
        NavigationManager navi { get; set; }



        private string nametext { get; set; }

        private bool userIsOwner { get; set; } = false;
        

        private UserDrinkListsDataModel? model { get; set; }

		private List<DrinkDisplayModel> drinks { get; set; } = new List<DrinkDisplayModel>();

        private List<string> tagsList = new List<string>();

        private bool _isOpen = false;

		private async Task GetData()
		{
            model = new UserDrinkListsDataModel();
            var newlist = await repo.GetList(ListId);

            if (newlist != null)
            {
                if (newlist.User.UserDisplayName == UserDisplayName)
                {

					drinks.Clear();
                    model = newlist;
					foreach (var drink in model.Drinks) 
					{
						var toadd = new DrinkDisplayModel();
						toadd.fromDrinkData(drink);
						drinks.Add(toadd);
					}

                }
            }
        }

		protected override async Task OnInitializedAsync()
		{
			await GetData();


            var tags = await repo.GetDrinkTags();

            
            tagsList.Clear();
            foreach (var tag in tags)
            {
                tagsList.Add(tag.Value);
            }

            userIsOwner = await IsUserMaker();
        }

        private async Task<bool> IsUserMaker()
        {
            if (authenticationState is not null)
            {
                var state = await authenticationState;

                var Username = state?.User?.Identity?.Name ?? string.Empty;

                if(Username != null)
                {
                    var user = await repo.GetUser(Username);
                    if(user.Id == model.User.Id)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        protected override async Task OnParametersSetAsync()
		{
            await GetData();
        }

        public void NavTo(string Name, int Id)
        {
            navi.NavigateTo($"/Drink/{Name.Replace("#", String.Empty)}/{Id}");
        }

		private void Toggle()
		{
			_isOpen = !_isOpen;
		}

        private async Task<IEnumerable<string>> Search(string value)
        {
            if (string.IsNullOrEmpty(value))
                return tagsList;
            return tagsList.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task SaveListName()
        {
            try
            {
                if (tagsList.Any(nametext.Contains))
                {
                    await repo.RenameDrinkList(model.User.Id, model.Id, nametext);

                    Toggle();
                    GetData();
                    StateHasChanged();
                    model.Name = nametext;
                }
            }
            catch { return; }

        }

        private async Task OnDelete(int drinkId)
        {
            if (await IsUserMaker())
            {
                var state = await authenticationState;
                var Username = state?.User?.Identity?.Name ?? string.Empty;

                await repo.RemoveDrinkFromDrinkList(Username, model.Id, drinkId);
            }

        }



    }
}
