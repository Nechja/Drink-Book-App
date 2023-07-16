using DataAccess.Models;
using DataAccess.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Drink_Book_App.Models
{
    public class GlassDisplayModel : DisplayDeleteProtection, IGlassDataModel
    {   
        public int Id { get; set; }
		[StringLength(50, MinimumLength = 2, ErrorMessage = "Size out of bounds.")]
		public string Name { get; set; }
		[Range(0, float.MaxValue, ErrorMessage = "Please enter valid Number")]
		[RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage= "Number format is X.XX")]
		public float? Oz { get; set; }
        public Uri? Image { get; set; }

        public GlassDisplayModel() 
        {
            Name = String.Empty;
        }

        public GlassDisplayModel(IGlassDataModel glass) 
        {
            Id = glass.Id;
            Name = glass.Name;
            Oz = glass.Oz;
            Image = glass.Image;
        }

        public GlassDataModel DataModel 
        {
            get
            {
                return new GlassDataModel()
                {
                    Id = Id,
                    Name = Name,
                    Oz = Oz,
                    Image = Image

                };
            }
        }
    }
}
