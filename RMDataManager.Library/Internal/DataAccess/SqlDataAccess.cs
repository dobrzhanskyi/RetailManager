using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace RMDataManager.Library.Internal.DataAccess
{
	internal class SqlDataAccess : IDisposable
	{
		private IDbConnection _connection;
		private IDbTransaction _transaction;

		public string GetConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				List<T> rows = connection.Query<T>(storedProcedure, parameters,
					commandType: CommandType.StoredProcedure).ToList();

				return rows;
			}
		}

		public void SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
		{
			string connectionString = GetConnectionString(connectionStringName);
			using (IDbConnection connection = new SqlConnection(connectionString))
			{
				connection.Execute(storedProcedure, parameters,
				   commandType: CommandType.StoredProcedure);
			}
		}

		public void StartTransaction(string connectionStringName)
		{
			var connectionString = GetConnectionString(connectionStringName);
			_connection = new SqlConnection(connectionString);
			_connection.Open();
			_transaction = _connection.BeginTransaction();
			_isClosed = false;
		}

		public void SaveDataInTransaction<T>(string storedProcedure, T parameters)
		{
			_connection.Execute(storedProcedure, parameters,
			   commandType: CommandType.StoredProcedure, transaction: _transaction);
		}

		public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
		{
			List<T> rows = _connection.Query<T>(storedProcedure, parameters,
				commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();

			return rows;
		}

		private bool _isClosed = false;

		public void CommitTransaction()
		{
			_transaction?.Commit();
			_connection?.Close();

			_isClosed = true;
		}

		public void RollbackTransaction()
		{
			_transaction?.Rollback();
			_connection?.Close();

			_isClosed = true;
		}

		public void Dispose()
		{
			if (!_isClosed)
			{
				try
				{
					CommitTransaction();
				}
				catch (Exception)
				{
					//TODO Log this issue
				}
			}

			_transaction = null;
			_connection = null;
		}
	}
}