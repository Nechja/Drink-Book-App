using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DataAccessTests.TestModels
{
	internal class Unit
	{
		[ExplicitKey]
		public int id { get; set; }
		public string name { get; set; }
		public Unit(string Name)
		{
			name = Name;
		}

	}
}
