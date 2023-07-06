using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using static MudBlazor.Colors;

namespace Drink_Book_App.Models
{
	public class FlagDisplayModel : IFlagDataModel
	{
		public int id { get; set; }
		[Required]
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string Name { get; set; }

		[Required]
		public FlagType? Flag { get; set; }
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string? OpeningStatement { get; set; }
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string? ClosingStatment { get; set; }
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string? InlineStatement { get; set; }

		public FlagDisplayModel() { }

		public FlagDisplayModel(IFlagDataModel flagData)
		{
			Name = flagData.Name;
			id = flagData.id;
			ClosingStatment = flagData.ClosingStatment;
			InlineStatement = flagData.InlineStatement;
			OpeningStatement = flagData.OpeningStatement;
			if (flagData is ServingFlagDataModel)
			{
				Flag = FlagType.Serving;
			}
			if (flagData is ShakerFlagDataModel)
			{
				Flag = FlagType.Shaker;
			}

		}

		public void SetFlagString(string value)
		{
			switch (value)
			{
				case "Serving":
					Flag = FlagType.Serving; break;
				case "Shaker":
					Flag = FlagType.Shaker; break;
				default:
					return;
			}

		}

		public string FlagString
		{
			get 
			{
				switch (Flag)
				{
					case FlagType.Serving:
						return "Serving";
					case FlagType.Shaker:
						return "Shaker";
				}
				return "";
			}
		}
	}
	public enum FlagType
	{
		Serving,
		Shaker
	}
}
