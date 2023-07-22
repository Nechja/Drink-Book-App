using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class LinkTypeDataModel
    {
        [Key]
        public int Id { get; set; }
        
        public string Type { get; set; } = string.Empty;
        public string Info { get; set; } = string.Empty;

        public List<LinkDataModel> Links { get; set; }

        public LinkTypeDataModel() { }
    }
}
