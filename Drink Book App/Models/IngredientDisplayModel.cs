using DataAccess.Models;
using DataAccess.Models.Interfaces;

namespace Drink_Book_App.Models
{
    public class IngredientDisplayModel : IIngredientDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IngredientTypeDisplayModel IngredientType { get; set; }

        public List<TagDisplayModel> Tags { get; set; } = new List<TagDisplayModel>();

        public IngredientDisplayModel(IngredientDataModel model)
        {
            Id = model.Id;
            Name = model.Name;
            IngredientType = new IngredientTypeDisplayModel(model.IngredientType);
            foreach (ITagDataModel tag in model.Tags)
            {
                Tags.Add(new TagDisplayModel(tag));
            }
        }

        public IngredientDisplayModel() { }
    }
}
