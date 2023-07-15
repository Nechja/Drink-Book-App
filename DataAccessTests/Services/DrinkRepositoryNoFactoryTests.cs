using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Models;

namespace DataAccess.Services.Tests
{
	[TestClass()]
	public class DrinkRepositoryNoFactoryTests
	{
		private DrinkDataModel _drink;

		[TestMethod()]
		public void DrinkRepositoryNoFactoryTest()
		{
			DrinkDBContext dbContext = new DrinkDBContext();
			try
			{
				DrinkRepositoryNoFactory repo = new DrinkRepositoryNoFactory(dbContext);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
}