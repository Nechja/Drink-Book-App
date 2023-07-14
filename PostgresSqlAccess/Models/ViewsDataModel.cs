using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class ViewsDataModel
    {
        [Key]
        public int Id { get; set; }
        public int Views { get; set; }

        [ForeignKey(nameof(Views))]
        public DrinkDataModel Drink { get; set; }

    }
}
