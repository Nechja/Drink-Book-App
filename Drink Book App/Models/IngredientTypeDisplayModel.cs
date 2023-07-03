using DataAccess.Models;
using DataAccess.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models
{
	public class IngredientTypeDisplayModel : IIngredientTypeDataModel
    {
		
		public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
        public string Name { get; set; }

        public IngredientTypeDisplayModel(IIngredientTypeDataModel model) 
        {
            Id = model.Id;
            Name = model.Name;
        }

        public IngredientTypeDisplayModel() { }

        public IngredientTypeDataModel DataModel 
        { 
            get 
            {
                return new IngredientTypeDataModel() { Id = Id, Name = Name };
            } 
        }

    }
}
