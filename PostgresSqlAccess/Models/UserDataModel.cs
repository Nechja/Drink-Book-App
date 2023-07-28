using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserDataModel: Logged
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public bool DarkMode { get; set; } = true;

        public List<UserDrinkListsDataModel> DrinkLists { get; set; } = new();
    }
}
