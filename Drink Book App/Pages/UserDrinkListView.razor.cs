using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
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

        [Inject]
        NavigationManager navi { get; set; }
        

        private UserDrinkListsDataModel? model { get; set; }

		private List<DrinkDisplayModel> drinks { get; set; } = new List<DrinkDisplayModel>();

		private bool _isOpen = false;

		private async Task GetData()
		{
            var newlist = await repo.GetList(ListId);

            if (newlist != null)
            {
                if (newlist.User.UserDisplayName == UserDisplayName)
                {
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




    }
}
