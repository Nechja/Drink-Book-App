using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.Interfaces;

namespace DataAccess.Models
{
    public class GlassDataModel : Logged, IGlassDataModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public float? Oz { get; set; }

        public string FontAwesomeIcon { get; set; }

        public Uri? Image { get; set; }

		public List<DrinkDataModel> Drinks { get; set; } = new();
    }
}
