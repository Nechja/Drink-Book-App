﻿using DataAccess.Models.Interfaces;
using DataAccess.Services;
using Drink_Book_App.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using MudBlazor;

namespace Drink_Book_App.Components.DrinkAddEdit
{
    public partial class AEDrinkCard
    {
		[Inject]
		public DrinkRepository repo { get; set; }

		[Inject]
        ISnackbar Snackbar { get; set; }

        public DrinkDisplayModel Drink { get; set; } = new DrinkDisplayModel();
        public InstructionDisplayModel Instruction { get; set; } = new InstructionDisplayModel();
		public List<GlassDisplayModel> Glassware { get; set; } = new List<GlassDisplayModel>();

		public List<(TagDisplayModel garnish, bool selected)> GarnishTypes { get; set; } = new List<(TagDisplayModel, bool)>();

		public List<TagDisplayModel> RimTypes { get; set; } = new List<TagDisplayModel>();

		public List<TagDisplayModel> IceTypes { get; set; } = new List<TagDisplayModel>();

		[Parameter]
		public int? DrinkId { get; set; }



		private MudChip[] _selected;

		private MudChip[] Selected
		{
			get => _selected;
			set
			{
				_selected= value;
				OnSelectGarnish(value);
			}
		}



		bool FakeSubmit = false;

		string ErrorText { get; set; } = string.Empty;

		protected override void OnInitialized()
        {
			Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;

            if (DrinkId != null || DrinkId == 0)
			{
				var d = repo.GetDrinkById(DrinkId.Value);
				if (d != null) Drink.fromDrinkData(d);

			}
			DataUpdate();
		}

		public void DataUpdate()
		{
			GlassUpdate();
			GarnishUpdate();
			IceUpdate();
			RimUpdate();
		}

		private bool openGlass;

		private bool OpenGlass
		{
			get => openGlass;
			set
			{
				openGlass = value;
				if (!openGlass)
				{
					GlassUpdate();
				}
			}
		}

		public void GlassUpdate()
		{
			Glassware.Clear();
			var glassdata = repo.GetGlassware();
			foreach (var glass in glassdata)
			{
				Glassware.Add(new GlassDisplayModel(glass));
			}
		}

		private bool openGarnish;

		private bool OpenGarnish
		{
			get => openGarnish;
			set 
			{ 
				openGarnish = value;
				if(!openGarnish) 
				{
					GarnishUpdate();
				}
			}
		}

		public void GarnishUpdate()
		{
			GarnishTypes.Clear();
			var garnishdata = repo.GetGarnishTypes();
			List<TagDisplayModel> selectedgarnish = new List<TagDisplayModel>();
			if(Drink.Garnishes.Any())
			{
				foreach(var g in Drink.Garnishes)
				{
					selectedgarnish.Add(g);
				}
			}
			foreach (var garnish in garnishdata)
			{
				if(selectedgarnish.FirstOrDefault(i => i.Id == garnish.Id) is null)
				{
					GarnishTypes.Add((new TagDisplayModel(garnish), false));
				}
				else
				{
					GarnishTypes.Add((new TagDisplayModel(garnish), true));
				}
				
			}
		}

		private bool openIce;

		private bool OpenIce
		{
			get => openIce;
			set
			{
				openIce = value;
				if (!openIce)
				{
					IceUpdate();
				}
			}
		}

		public void IceUpdate()
		{
			IceTypes.Clear();
			var icedata = repo.GetIceTypes();
			foreach (var ice in icedata)
			{
				IceTypes.Add(new TagDisplayModel(ice));
			}
		}

		private bool openRim;

		private bool OpenRim
		{
			get => openRim;
			set
			{
				openRim = value;
				if (!openRim)
				{
					RimUpdate();
				}
			}
		}

		public void RimUpdate()
		{
			var rimdata = repo.GetRimTypes();
			foreach (var rim in rimdata)
			{
				RimTypes.Add(new TagDisplayModel(rim));
			}
		}

		protected void OnValidSubmit()
		{
			try
			{
                ErrorText = string.Empty;
                if (Drink.Instructions.Count < 1)
                {
                    ErrorText = "Must have 1 or more Instructions";
                    return;
                }
                if (FakeSubmit)
                {
                    FakeSubmit = false;
                    return;
                }
                if (Drink.Id == 0)
                {
                    repo.AddDrink(Drink.GetDataModel());
                    Snackbar.Add($"Drink {Drink.Name} Added");
                    Drink = new DrinkDisplayModel();
                }
                else
                {
                    repo.UpdateDrink(Drink.GetDataModel());
                    Snackbar.Add($"Drink {Drink.Name} Updated");
                    DataUpdate();
                    Drink = new DrinkDisplayModel();

                }
            }
			catch (Exception ex)
			{
                Snackbar.Clear();
                Snackbar.Add($"Error Adding Drink {ex.Message}");
            }

		}

		protected void OnSelectInstructionChange(InstructionDisplayModel m)
        {
			if(Instruction.Id == 0)
			{
				Drink.Instructions.Add(m);
			}
			else
			{
				var index = Drink.Instructions.IndexOf(m);
				Drink.Instructions[index] = m;
			}
            
            Instruction = new InstructionDisplayModel();
        }

		private void OnSelectTag(List<TagDisplayModel> tags)
		{
			Drink.Tags = tags;
		}

		private void OnSelectGarnish(MudChip[] garnish)
		{
			Drink.Garnishes.Clear();
			var glist = new List<TagDisplayModel>();
			foreach (MudChip g in garnish)
			{
				glist.Add((TagDisplayModel)g.Value);
			}
			 Drink.Garnishes = glist;
		}
		private void OnEnterStop(bool s)
		{
			FakeSubmit = s;
		}

		protected void KeyStop(KeyboardEventArgs e)
		{
			if (e.Code == "Enter" || e.Code == "NumpadEnter") FakeSubmit = true;
		}

		protected void DeleteInstruction(InstructionDisplayModel m)
		{

				Drink.Instructions.Remove(m);
                Snackbar.Add($"Instruction Deleted. Staged.");



			

        }
	}
}
