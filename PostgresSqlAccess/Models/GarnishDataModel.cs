using DataAccess.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    [Index(nameof(Value), IsUnique = true)]
    public class GarnishDataModel : ITagDataModel
    {
        [Key]
        public int Id { get; set; }

        public string Value { get; set; }

        public List<DrinkDataModel> Drinks { get; set; } = default;

        public GarnishDataModel() { }

    }
}
