﻿using AltnCrossAPI.Database.Interfaces;
using AltnCrossAPI.Shared;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database
{
	public class DBHelper//: IDBHelper
	{
		#region Class Member Declarations
		private SqlConnection _dBConnection;
		private bool _isTransactionPending, _isDisposed;
		private SqlTransaction _currentTransaction;
		private ArrayList _savePoints;
		#endregion

		#region Class Property Declarations
		public SqlTransaction CurrentTransaction
		{
			get
			{
				return _currentTransaction;
			}
		}


		public bool IsTransactionPending
		{
			get
			{
				return _isTransactionPending;
			}
		}


		public SqlConnection DBConnection
		{
			get
			{
				return _dBConnection;
			}
		}
		#endregion

		internal DBHelper()
        {
			// Init the class
			InitClass();
		}

        #region DB Methods
        /// <summary>
        /// Purpose: Implements the IDispose' method Dispose.
        /// </summary>
        internal void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Purpose: Initializes class members.
		/// </summary>
		private void InitClass()
		{
			// create all the objects and initialize other members.
			_dBConnection = new SqlConnection();

			// Set connection string of the SqlConnection object
			_dBConnection.ConnectionString = ConfigHelper.ConnectionString;
			_isDisposed = false;
			_currentTransaction = null;
			_isTransactionPending = false;
			_savePoints = new ArrayList();
		}


		/// <summary>
		/// Purpose: Implements the Dispose functionality.
		/// </summary>
		protected virtual void Dispose(bool isDisposing)
		{
			// Check to see if Dispose has already been called.
			if (!_isDisposed)
			{
				if (isDisposing)
				{
					// Dispose managed resources.
					if (_currentTransaction != null)
					{
						_currentTransaction.Dispose();
						_currentTransaction = null;
					}
					if (_dBConnection != null)
					{
						// closing the connection will abort (rollback) any pending transactions
						_dBConnection.Close();
						_dBConnection.Dispose();
						_dBConnection = null;
					}
				}
			}
			_isDisposed = true;
		}



		/// <summary>
		/// Purpose: opens the connection object.
		/// </summary>
		/// <returns>true, if succeeded, otherwise an Exception exception is thrown.</returns>
		internal bool OpenConnection()
		{
			try
			{
				if ((_dBConnection.State & ConnectionState.Open) > 0)
				{
					// it's already open.
					throw new Exception("OpenConnection::Connection is already open.");
				}
				_dBConnection.Open();
				_isTransactionPending = false;
				return true;
			}
			catch (Exception ex)
			{
				// bubble exception
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: starts a new ADO.NET transaction using the open connection object of this class.
		/// </summary>
		/// <param name="transactionName">Name of the transaction to start</param>
		/// <returns>true, if transaction is started correctly, otherwise an Exception exception is thrown</returns>
		internal bool BeginTransaction(string transactionName)
		{
			try
			{
				if (_isTransactionPending)
				{
					// no nested transactions allowed.
					throw new Exception("BeginTransaction::Already transaction pending. Nesting not allowed");
				}
				if ((_dBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("BeginTransaction::Connection is not open.");
				}
				// begin the transaction and store the transaction object.
				_currentTransaction = _dBConnection.BeginTransaction(IsolationLevel.ReadCommitted);
				_isTransactionPending = true;
				_savePoints.Clear();
				return true;
			}
			catch (Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: commits a pending transaction on the open connection object of this class.
		/// </summary>
		/// <returns>true, if commit was succesful, or an Exception exception is thrown</returns>
		internal bool CommitTransaction()
		{
			try
			{
				if (!_isTransactionPending)
				{
					// no transaction pending
					throw new Exception("CommitTransaction::No transaction pending.");
				}
				if ((_dBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("CommitTransaction::Connection is not open.");
				}
				// commit the transaction
				_currentTransaction.Commit();
				_isTransactionPending = false;
				_currentTransaction.Dispose();
				_currentTransaction = null;
				_savePoints.Clear();
				return true;
			}
			catch (Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: rolls back a pending transaction on the open connection object of this class, 
		/// or rolls back to the savepoint with the given name. Savepoints are created with SaveTransaction().
		/// </summary>
		/// <param name="transactionToRollback">Name of transaction to roll back. Can be name of savepoint</param>
		/// <returns>true, if rollback was succesful, or an Exception exception is thrown</returns>
		internal bool RollbackTransaction(string transactionToRollback)
		{
			try
			{
				if (!_isTransactionPending)
				{
					// no transaction pending
					throw new Exception("RollbackTransaction::No transaction pending.");
				}
				if ((_dBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("RollbackTransaction::Connection is not open.");
				}
				// rollback the transaction
				_currentTransaction.Rollback();
				// if this wasn't a savepoint, we've rolled back the complete transaction, so we
				// can clean it up.
				if (!_savePoints.Contains(transactionToRollback))
				{
					// it's not a savepoint
					_isTransactionPending = false;
					_currentTransaction.Dispose();
					_currentTransaction = null;
					_savePoints.Clear();
				}
				return true;
			}
			catch (Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: Saves a pending transaction on the open connection object of this class to a 'savepoint'
		/// with the given name.
		/// When a rollback is issued, the caller can rollback to this savepoint or roll back the complete transaction.
		/// </summary>
		/// <param name="savePointName">Name of the savepoint to store the current transaction under.</param>
		/// <returns>true, if save was succesful, or an Exception exception is thrown</returns>
		internal bool SaveTransaction(string savePointName)
		{
			try
			{
				if (!_isTransactionPending)
				{
					// no transaction pending
					throw new Exception("SaveTransaction::No transaction pending.");
				}
				if ((_dBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					throw new Exception("SaveTransaction::Connection is not open.");
				}
				// save the transaction
				//_currentTransaction.Save(savePointName);
				// Store the savepoint in the list.
				_savePoints.Add(savePointName);
				return true;
			}
			catch (Exception ex)
			{
				// bubble error
				throw ex;
			}
		}


		/// <summary>
		/// Purpose: Closes the open connection. Depending on bCommitPendingTransactions, a pending
		/// transaction is commited, or aborted. 
		/// </summary>
		/// <param name="commitPendingTransaction">Flag for what to do when a transaction is still pending. True
		/// will commit the current transaction, false will abort (rollback) the complete current transaction.</param>
		/// <returns>true, if close was succesful, false if connection was already closed, or an Exception exception is thrown when
		/// an error occurs</returns>
		internal bool CloseConnection(bool commitPendingTransaction)
		{
			try
			{
				if ((_dBConnection.State & ConnectionState.Open) == 0)
				{
					// no open connection
					return false;
				}
				if (_isTransactionPending)
				{
					if (commitPendingTransaction)
					{
						// commit the pending transaction
						_currentTransaction.Commit();
					}
					else
					{
						// rollback the pending transaction
						_currentTransaction.Rollback();
					}
					_isTransactionPending = false;
					_currentTransaction.Dispose();
					_currentTransaction = null;
					_savePoints.Clear();
				}
				// close the connection
				_dBConnection.Close();
				return true;
			}
			catch (Exception ex)
			{
				// bubble error
				throw ex;
			}
		}

		internal DataTable ExecuteProcedure(string sprocName, SqlParameter[] parameters)
		{
			SqlCommand cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = sprocName;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.CommandTimeout = 0;
			cmdToExecute.Connection = DBConnection;
			SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
			try
			{
				cmdToExecute.Parameters.AddRange(parameters);

				OpenConnection();

				DataTable tableToReturn = new DataTable();
				SqlDataAdapter adp = new SqlDataAdapter(cmdToExecute);
				adapter.Fill(tableToReturn);
				return tableToReturn;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				CloseConnection(false);
				cmdToExecute.Dispose();
				adapter.Dispose();
			}
		}
		
		internal void ExecuteNonQuery(string sprocName, SqlParameter[] parameters)
		{
			SqlCommand cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = sprocName;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.CommandTimeout = 0;
			cmdToExecute.Connection = DBConnection;
			try
			{
				cmdToExecute.Parameters.AddRange(parameters);

				OpenConnection();

				cmdToExecute.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				CloseConnection(false);
				cmdToExecute.Dispose();
			}
		}

		internal int ExecuteReader(string sprocName, SqlParameter[] parameters, string outParam)
		{
			SqlCommand cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = sprocName;
			cmdToExecute.CommandType = CommandType.StoredProcedure;
			cmdToExecute.CommandTimeout = 0;
			cmdToExecute.Connection = DBConnection;
			try
			{
				cmdToExecute.Parameters.AddRange(parameters);
				cmdToExecute.Parameters[outParam].Direction = ParameterDirection.Output;

				OpenConnection();

				cmdToExecute.ExecuteReader();
				return Convert.ToInt32(cmdToExecute.Parameters["@Id"].Value);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				CloseConnection(false);
				cmdToExecute.Dispose();
			}
		}

		internal T ExecuteReaderQuery<T>(string query, SqlParameter[] parameters, T defaultValue = default(T))
		{
			SqlCommand cmdToExecute = new SqlCommand();
			cmdToExecute.CommandText = query;
			cmdToExecute.CommandType = CommandType.Text;
			cmdToExecute.CommandTimeout = 0;
			cmdToExecute.Connection = DBConnection;
			try
			{
				cmdToExecute.Parameters.AddRange(parameters);

				OpenConnection();

				var dr = cmdToExecute.ExecuteReader();

				return dr.Read() ? (T)dr[0] : defaultValue;
            }
            catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				CloseConnection(false);
				cmdToExecute.Dispose();
			}
		}
		#endregion
	}
}