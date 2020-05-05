using System.Data;
using System.Data.SqlClient;

namespace AltnCrossAPI.Database.Interfaces
{
	internal interface IDBHelper
	{
		void Dispose();
		void InitClass();
		void Dispose(bool isDisposing);
		bool OpenConnection();
		bool BeginTransaction(string transactionName);
		bool CommitTransaction();
		bool RollbackTransaction(string transactionToRollback);
		bool SaveTransaction(string savePointName);
		bool CloseConnection(bool commitPendingTransaction);
		DataTable ExecuteProcedure(string sprocName, SqlParameter[] parameters);
		void ExecuteNonQuery(string sprocName, SqlParameter[] parameters);
		int ExecuteReader(string sprocName, SqlParameter[] parameters, string outParam);
		string ExecuteReaderQuery(string query, SqlParameter[] parameters);
	}
}