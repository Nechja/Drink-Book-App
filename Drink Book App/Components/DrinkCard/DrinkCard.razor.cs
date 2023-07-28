using DataAccess.Models;
using DataAccess.Models.Interfaces;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Xml;

namespace Drink_Book_App.Components.DrinkCard
{
	partial class DrinkCard
	{
        [Inject]
        NavigationManager navi { get; set; }

        [Parameter]
		public DrinkDataModel DrinkData { get; set; } = default!;
		public DrinkDisplayModel DrinkDisplay { set; get; } = new DrinkDisplayModel();
		public List<InstructionDisplayModel> InstructionsList { get; set; } = new List<InstructionDisplayModel>();

		protected override void OnInitialized()
		{
			if (DrinkData != null)
			{
				DrinkDisplay.fromDrinkData(DrinkData);

				foreach(InstructionDataModel instruction in DrinkData.Instructions) 
				{
					InstructionDisplayModel instructioddisplay = new InstructionDisplayModel(instruction);

					InstructionsList.Add(instructioddisplay);
				}
			}
		}

        public void EditDrink(int Id)
        {
            navi.NavigateTo($"/drinktools/editdrink/{Id}");
        }
    }
}
