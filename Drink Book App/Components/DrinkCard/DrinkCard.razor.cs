using DataAccess.Models;
using DataAccess.Models.Interfaces;
using DataAccess.Services;
using Drink_Book_App.Components.DrinkList;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;

namespace Drink_Book_App.Components.DrinkCard
{
	partial class DrinkCard
	{
        [Inject]
        NavigationManager navi { get; set; }

        [Inject]
        DrinkRepositoryAsync repo { get; set; }

        [Parameter]
		public DrinkDataModel DrinkData { get; set; } = default!;
		public DrinkDisplayModel DrinkDisplay { set; get; } = new DrinkDisplayModel();
		public List<InstructionDisplayModel> InstructionsList { get; set; } = new List<InstructionDisplayModel>();

        [CascadingParameter]
        private Task<AuthenticationState>? authenticationState { get; set; }

        private List<UserDrinkListsDataModel> drinkLists { get; set; } = new();

        private string Username;

        private bool _isOpen = false;

        public void ToggleOpen()
        {
            if (_isOpen)
                _isOpen = false;
            else
                _isOpen = true;
        }

        protected override async Task OnInitializedAsync()
		{
			if (DrinkData != null)
			{
				DrinkDisplay.fromDrinkData(DrinkData);

				foreach(InstructionDataModel instruction in DrinkData.Instructions) 
				{
					InstructionDisplayModel instructioddisplay = new InstructionDisplayModel(instruction);

					InstructionsList.Add(instructioddisplay);
				}

                if (authenticationState is not null)
                {
                    var state = await authenticationState;

                    Username = state?.User?.Identity?.Name ?? string.Empty;

                    if (Username == "") { return; }
                    drinkLists = await repo.GetLists(Username);
                }
            }
		}

        public void EditDrink(int Id)
        {
            navi.NavigateTo($"/drinktools/editdrink/{Id}");
        }

		public async Task AddToList(string listname, int listid)
		{
            await repo.AppendDrinkList(Username, listid, DrinkDisplay.GetDataModel());
            ToggleOpen();
		}
    }
}
