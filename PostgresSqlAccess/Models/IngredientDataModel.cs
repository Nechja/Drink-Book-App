﻿using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models
{
	[Index(nameof(Name), IsUnique = true)]
	public class IngredientDataModel : IIngredientDataModel
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public IngredientTypeDataModel IngredientType { get; set; } = new();



        public List<InstructionDataModel> Instructions { get; set; } = new();
		public List<IngredientTagDataModel> Tags { get; set; } = new();

		public DateTime? Created { get; set; }
	}
}
