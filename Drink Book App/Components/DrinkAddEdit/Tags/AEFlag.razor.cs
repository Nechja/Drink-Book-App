using DataAccess.Models;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using MudBlazor;

namespace Drink_Book_App.Components.DrinkAddEdit.Tags
{
    public partial class AEFlag
    {
		[Inject]
		public DrinkRepositoryAsync repo { get; set; }
		public List<FlagDisplayModel> Flags { get; set; } = new List<FlagDisplayModel>();
        public FlagDisplayModel FlagDisplay { get; set; } = new FlagDisplayModel();
        string ErrorText { get; set; } = string.Empty;
		
        private MudChip selected;

        private MudChip Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (selected != null)
                {
                    ChipChanged(value);
                }
            }
        }

		protected override async Task OnInitializedAsync()
		{
             await UpdateFlags();

		}

        protected async Task UpdateFlags()
        {
            Flags.Clear();
			var flaglist = await repo.GetFlags();
			foreach (var flag in flaglist)
			{
				Flags.Add(new FlagDisplayModel(flag));
			}

		}

		protected async Task OnValidSubmit()
        {
            if(FlagDisplay.id  == 0)
            {

				await repo.AddFlag(new FlagDataModel
				{
					id = FlagDisplay.id,
					Name = FlagDisplay.Name,
					OpeningStatement = FlagDisplay.OpeningStatement,
					ClosingStatment = FlagDisplay.ClosingStatment,
					InlineStatement = FlagDisplay.InlineStatement
				});
				
			}
			else
			{
				await repo.UpdateFlag(new FlagDataModel
				{
					id = FlagDisplay.id,
					Name = FlagDisplay.Name,
					OpeningStatement = FlagDisplay.OpeningStatement,
					ClosingStatment = FlagDisplay.ClosingStatment,
					InlineStatement = FlagDisplay.InlineStatement
				});
			}


			await UpdateFlags();
			FlagDisplay = new FlagDisplayModel();
            

        }

        protected void EditFlag(FlagDisplayModel f)
        {
            FlagDisplay = f;
        }

        protected void ChipChanged(MudChip chip)
        {

        }
    }
}
