using DataAccess.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class TagDataModel: ITagDataModel
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; } = string.Empty;
        public TagTypeDataModel Type { get; set; } = new TagTypeDataModel();

        public TagDataModel() { }


    }
}
