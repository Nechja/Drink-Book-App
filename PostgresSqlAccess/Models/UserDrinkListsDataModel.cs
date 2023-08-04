using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserDrinkListsDataModel: Logged
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }

        public List<DrinkDataModel> Drinks { get; set; } = new();

        public UserDataModel User { get; set; } = new();

        public UserDrinkListsDataModel() { }
    }
}
