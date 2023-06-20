using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services;
public class SqlDataAccess
    {
	private readonly string connectionString;

	public string ConnectionStringName { get; set; } = "Testing";

	/// <summary>
	/// Initializes a new instance of the SQLDataAccess class using an IConfiguration object.
	/// It retrieves the connection string from the configuration based on the specified ConnectionStringName.
	/// </summary>
	/// <param name="config">The IConfiguration object containing the connection string.</param>
	public SqlDataAccess(IConfiguration config)
	{
		connectionString = config.GetConnectionString(ConnectionStringName)!;
	}
	/// <summary>
	/// Initializes a new instance of the SQLDataAccess class using a connection string. 
	/// This was built for unit testing the system.
	/// </summary>
	/// <param name="config">The connection string to be used for database access.</param>
	public SqlDataAccess(string config)
	{
		connectionString = config;

	}

	/// <summary>
	/// Executes an SQL query and retrieves a list of objects from the database.
	/// The query is executed using the provided connection string, and the parameters are passed to the query.
	/// </summary>
	/// <typeparam name="T">The type of the objects to retrieve.</typeparam>
	/// <typeparam name="U">The type of the query parameters.</typeparam>
	/// <param name="sql">The SQL query to execute.</param>
	/// <param name="parameters">The parameters to be passed to the query.</param>
	/// <returns>A list of objects retrieved from the database.</returns>
	public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
	{
		try
		{

			using (IDbConnection connection = new NpgsqlConnection(connectionString))
			{
				var data = await connection.QueryAsync<T>(sql, parameters);

				return data.ToList();
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	/// <summary>
	/// Executes an SQL query to save data to the database.
	/// The query is executed using the provided connection string, and the parameters are passed to the query.
	/// </summary>
	/// <typeparam name="T">The type of the parameters to be passed to the query.</typeparam>
	/// <param name="sql">The SQL query to execute.</param>
	/// <param name="parameters">The parameters to be passed to the query.</param>
	public async Task SaveData<T>(string sql, T parameters)
	{

		using (IDbConnection connection = new NpgsqlConnection(connectionString))
		{
			var data = await connection.ExecuteAsync(sql, parameters);
		}
	}

	public async Task<int> SaveDataWithCheck<T>(string sql, T parameters)
	{
		throw new NotImplementedException();

		using (IDbConnection connection = new NpgsqlConnection(connectionString))
		{
			var data = await connection.ExecuteAsync(sql, parameters);
			return 0;
		}
	}

	/// <summary>
	/// Executes an SQL query to delete data from the database.
	/// The query is executed using the provided connection string, and no parameters are passed to the query.
	/// </summary>
	/// <param name="sql">The SQL query to execute.</param>
	public async Task DeleteData(string sql)
	{
		using (IDbConnection connection = new NpgsqlConnection(connectionString))
		{
			var data = await connection.ExecuteAsync(sql);
		}
	}

	public async Task UpdateData(string sql,int id)
	{
		using (IDbConnection connection = new NpgsqlConnection(connectionString))
		{
			var data = await connection.ExecuteAsync(sql,id);
		}
	}
}
