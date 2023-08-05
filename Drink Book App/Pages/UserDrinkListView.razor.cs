using DataAccess.Models;
using DataAccess.Services;
using Microsoft.AspNetCore.Components;

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

		[Inject]
		public DrinkRepositoryAsync repo { get; set; }

		private UserDrinkListsDataModel? model { get; set; }

		private async Task GetData()
		{
            var newlist = await repo.GetList(ListId);

            if (newlist != null)
            {
                if (newlist.User.UserDisplayName == UserDisplayName)
                {
                    model = newlist;
                }
            }
        }

		protected override async Task OnInitializedAsync()
		{
			await GetData();

		}

        protected override async Task OnParametersSetAsync()
		{
            await GetData();
        }




    }
}
