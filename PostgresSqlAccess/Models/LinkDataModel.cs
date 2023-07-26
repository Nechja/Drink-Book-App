using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class LinkDataModel
    {
        [Key] public int Id { get; set; }

        public Uri Link { get; set; }

        public int Clicks { get; set; }



		public LinkTypeDataModel Type { get; set; } = new LinkTypeDataModel();

        public List<IngredientDataModel> Ingredients { get; set; }

        public LinkDataModel() { }



    }
}
