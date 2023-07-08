using DataAccess.Models;
using DataAccess.Models.Interfaces;
using MudBlazor;
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

		}

		public FlagDataModel ToDataModel()
		{
			return new FlagDataModel
			{
				Name = Name,
				ClosingStatment = ClosingStatment,
				InlineStatement = InlineStatement,
				OpeningStatement = OpeningStatement, 
				id = id
			};
		}

		public static implicit operator FlagDisplayModel(MudChip v)
		{
			throw new NotImplementedException();
		}
	}
}
