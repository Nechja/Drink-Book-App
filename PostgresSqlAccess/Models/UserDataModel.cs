using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class UserDataModel: Logged
    {
        [Key]
        public int Id { get; set; }

        public byte[] UserName { get; set; }

        public string SetUserName
        {
            set 
            { 
                using(SHA256 sha256 = SHA256.Create())
                {
                    byte[] un = sha256.ComputeHash(Encoding.ASCII.GetBytes(value));
                } 
            }
        }

        public string UserDisplayName { get; set; }
        public bool DarkMode { get; set; } = true;

        public List<UserDrinkListsDataModel> DrinkLists { get; set; } = new();
    }
}
