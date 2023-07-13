using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Index(nameof(Name), IsUnique = true)]
	public class FlagDataModel : IFlagDataModel
	{
		[Key]
		public int id { get; set; }
		public string? Name { get; set; }

		public string? OpeningStatement { get; set; }

		public string? ClosingStatment { get; set; }

		public string? InlineStatement { get; set; }

		public DateTime? Created { get; set; }

		public List<InstructionDataModel>? Instructions { get; set; } = new();

		public FlagDataModel() { }
	}
}
