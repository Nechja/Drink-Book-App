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

		/// <summary>
		/// This test method verifies the functionality of the LoadData method in the SqlDataAccess class.
		/// It executes an SQL query to retrieve a list of Unit objects from the database and checks if the returned list is not null.
		/// If the list is null, the test fails.
		/// </summary>
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

		/// <summary>
		/// This test method verifies the functionality of the DeleteData method in the SqlDataAccess class.
		/// It creates a new unit with a random name, saves it to the database, retrieves it from the database to verify the write operation,
		/// deletes the unit from the database, and checks if the unit is successfully deleted by attempting to retrieve it again.
		/// If the write operation fails or the unit is not deleted, the test fails.
		/// </summary>
		[TestMethod()]
		public async Task DeleteDataTest()
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
				sql = $"delete from Unit where name = '{unit.name}';";
				testaccess.DeleteData(sql);

				sql = $"select * from Unit where name='{unit.name}';";
				List<Object> listofunitfordel = await testaccess.LoadData<Object, dynamic>(sql, new { });
				if (listofunitfordel.Count != 0)
				{
					Assert.Fail("Delete failed.");
				}

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}
		}

		[TestMethod()]
		public async Task UpdateDataTest()
		{
			try
			{
				SqlDataAccess testaccess = new SqlDataAccess(connectionstring);
				//set up random units
				string sql = @"insert into Unit (name) values (@name);";
				for (int i = 0; i < 4; i++)
				{
					Random rnd = new Random();
					Unit unit = new Unit(rnd.Next().ToString());
					await testaccess.SaveData(sql, unit);
				}
				sql = $"select * from Unit;";
				List<Unit> listofunit = await testaccess.LoadData<Unit, dynamic>(sql, new { });
				if (listofunit.Count == 0)
				{
					Assert.Fail("Write failed.");
				}
				int id = 0;
				foreach(Unit u in listofunit)
				{
					id = u.id;
				}
				if (id == 0)
				{
					Assert.Fail("Unit was not a list.");
				}
				sql = @"update Unit set name = 'Update Test'" +
					$" where id = {id}";
				await testaccess.UpdateData(sql,id);
				sql = $"select * from Unit where name='Update Test';";
				List<Object> listofunitfordel = await testaccess.LoadData<Object, dynamic>(sql, new { });
				if (listofunitfordel.Count != 1)
				{
					Assert.Fail("Update failed.");
				}


			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}
		}

		/// <summary>
		/// This method performs cleanup tasks for SQL tests.
		/// It deletes all data from the Unit table and restarts the sequence for the primary key column.
		/// This method is used to clean up the database after running SQL-related tests.
		/// </summary>
		/// 
		[TestMethod()]
		public async Task XCleanUpSql()
		{
			try
			{
				SqlDataAccess testaccess = new SqlDataAccess(connectionstring);
				string sql = "DELETE FROM Unit;" +
					"ALTER SEQUENCE Unit_id_seq RESTART WITH 1;";
				await testaccess.DeleteData(sql);

			}
			catch (Exception ex)
			{
				Assert.Fail(ex.ToString());
			}

		}

	}
}