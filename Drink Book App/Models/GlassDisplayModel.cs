using DataAccess.Models;
using DataAccess.Models.Interfaces;

namespace Drink_Book_App.Models
{
    public class GlassDisplayModel : IGlassDataModel
    {   
        public int Id { get; set; }
        public int Name { get; set; }
        public int? Oz { get; set; }
        public Uri? Image { get; set; }

        public GlassDisplayModel() { }

        public GlassDisplayModel(IGlassDataModel glass) 
        {
            Id = glass.Id;
            Name = glass.Name;
            Oz = glass.Oz;
            Image = glass.Image;
        }
    }
}
