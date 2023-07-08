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
		public DrinkRepository repo { get; set; }
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

		protected override void OnInitialized()
		{
             UpdateFlags();

		}

        protected private void UpdateFlags()
        {
            Flags.Clear();
			var flaglist = repo.GetFlags();
			foreach (var flag in flaglist)
			{
				Flags.Add(new FlagDisplayModel(flag));
			}

		}

		protected private void OnValidSubmit()
        {
            if(FlagDisplay.id  == 0)
            {

				repo.AddFlag(new FlagDataModel
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
				repo.UpdateFlag(new FlagDataModel
				{
					id = FlagDisplay.id,
					Name = FlagDisplay.Name,
					OpeningStatement = FlagDisplay.OpeningStatement,
					ClosingStatment = FlagDisplay.ClosingStatment,
					InlineStatement = FlagDisplay.InlineStatement
				});
			}


			UpdateFlags();
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
