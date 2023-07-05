using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models
{
    public class TagDisplayModel : ITagDataModel
    {
        public int Id { get; set; }
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
        public string Value { get; set; }


        public TagDisplayModel(ITagDataModel tagDataModel)
        {
            Id = tagDataModel.Id;
            Value = tagDataModel.Value;
        }

        public TagDisplayModel() { }

        public IngredientTagDataModel IngredientTagDataModel 
        {
            get 
            { 
                return new IngredientTagDataModel() 
                { 
                    Id = Id,
                    Value = Value
                }; 
            }
        }
    }
}
