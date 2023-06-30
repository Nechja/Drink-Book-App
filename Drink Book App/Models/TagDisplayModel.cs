using DataAccess.Models.Interfaces;

namespace Drink_Book_App.Models
{
    public class TagDisplayModel : ITagDataModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
