using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessTests.TestModels;
using System.Runtime.CompilerServices;

namespace DataAccess.Services.Tests
{
	[TestClass()]
	public class SqlDataAccessTests
	{
		string connectionstring = "Username=postgres;Password=2244;Host=172.31.48.1;Port=5432;DataBase=UnitTesting;Pooling=true;";


		/// <summary>
		/// This method performs a test to validate the functionality of the SqlDataAccess class by executing a simple SQL query.
		/// It retrieves a list of integers from the database and checks if the list is not null and contains at least one value.
		/// If the list is null or empty, the test fails.
		/// </summary>
		[TestMethod()]
		public async Task SqlDataAccessTest()
		{
			try
			{
				SqlDataAccess testaccess = new SqlDataAccess(connectionstring);
				string sql = "select 1;";
				List<int> listofunit = await testaccess.LoadData<int, dynamic>(sql, new { });
				Assert.IsNotNull(listofunit, "List is Returning Null");
				if (listofunit.Count == 0)
				{
					Assert.Fail("Server is not returning values");
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}


		[TestMethod()]
		public void LoadDataTest()
		{
			try
			{
				SqlDataAccess testaccess = new SqlDataAccess(connectionstring);
				string sql = "select * from Unit;";
				var listofunit = testaccess.LoadData<Unit, dynamic>(sql, new { });
				Assert.IsNotNull(listofunit);
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}
		}

		/// <summary>
		/// This method performs a test to save data to the database. It generates a new unit with a random name, 
		/// saves the unit to the database using an SQL query, and then retrieves the saved unit to verify the success of the write operation.
		/// If the unit is not found in the database, the test fails.
		/// </summary>
		[TestMethod()]
		public async Task SaveDataTest()
		{
			try
			{
				SqlDataAccess testaccess = new SqlDataAccess(connectionstring);
				Random rnd = new Random();
				Unit unit = new Unit(rnd.Next().ToString());
				string sql = @"insert into Unit (name) values (@name);";
				await testaccess.SaveData(sql, unit);
				sql = $"select * from Unit where name='{unit.name}';";
				List<Object> listofunit = await testaccess.LoadData<Object, dynamic>(sql, new { });
				if (listofunit.Count == 0)
				{
					Assert.Fail("Write failed.");
				}
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}
		}

		[TestMethod()]
		public void DeleteDataTest()
		{
			//Assert.Fail();
		}

		[TestMethod()]
		public void UpdateDataTest()
		{
			Assert.Fail();
		}
	}
}