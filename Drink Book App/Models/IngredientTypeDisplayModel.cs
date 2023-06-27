using DataAccess.Models.Interfaces;

namespace Drink_Book_App.Models
{
    public class IngredientTypeDisplayModel : IIngredientTypeDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IngredientTypeDisplayModel(IIngredientTypeDataModel model) 
        {
            Id = model.Id;
            Name = model.Name;
        }

        public IngredientTypeDisplayModel() { }

    }
}
