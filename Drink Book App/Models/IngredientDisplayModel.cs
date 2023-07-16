using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models;

    public class IngredientDisplayModel : DisplayDeleteProtection, IIngredientDataModel
    {
        public int Id { get; set; }
	    [Required]
	    [StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
	    public string Name { get; set; }

        [Required]
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
	private List<IngredientTagDataModel> TagLoader()
	{
		List<IngredientTagDataModel> tags = new List<IngredientTagDataModel>();
		foreach (var tag in Tags)
		{
			tags.Add(tag.IngredientTagDataModel);
		}
        return tags;
	}
	public IngredientDataModel DataModel 
        {
            get 
            {
            return new IngredientDataModel()
            {
                Id = Id,
                Name = Name,
                IngredientType = IngredientType.DataModel,
                Tags = TagLoader()
            };
            }
        }


}
