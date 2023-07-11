using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DataAccess.Models
{
    [Index(nameof(Value), IsUnique = true)]
    public class InstructionTagDataModel : ITagDataModel
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }

		public List<InstructionDataModel> Instructions { get; set; }

		public InstructionTagDataModel() { }

		public DateTime? Created { get; set; }

		public InstructionTagDataModel(string value)
		{
			Value = value;
		}
	}
}
