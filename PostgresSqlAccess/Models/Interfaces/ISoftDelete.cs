using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Interfaces
{
	public interface ISoftDelete
	{
		public bool IsDeleted { get; set; }

		public bool? FINALDELETE { get; set; }
		public DateTime? DeletedAt { get; set; }


	}
}
