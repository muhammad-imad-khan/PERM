using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Perm.Database.Audit
{
    public abstract class TriggerHandlerBase : IDisposable
    {
        protected const string InsertedQuery = "SELECT * FROM Inserted;";
        protected const string DeletedQuery = "SELECT * FROM Deleted;";
        protected abstract string GetTriggerDataQuery();

        public abstract List<AuditTrailModel> GetAuditModels(DataSet dataSet);
        SqlConnection _sqlCnn;

        private SqlConnection GetSqlConnection()
        {
            if (_sqlCnn == null)
            {
                _sqlCnn = new SqlConnection("context connection=true");
                _sqlCnn.Open();
                return _sqlCnn;
            }
            return _sqlCnn;
        }

        public void Handle()
        {
            DataSet dataSet = GetTriggerData();

            List<AuditTrailModel> auditTrails = GetAuditModels(dataSet);
            string insertStatement = auditTrails.GenerateScript();

            AddAuditRecord(insertStatement);
        }

        public void Dispose()
        {
            _sqlCnn?.Dispose();
        }

        private DataSet GetTriggerData()
        {
            SqlConnection sqlCnn = GetSqlConnection();
            SqlCommand cmdRead = new SqlCommand(GetTriggerDataQuery(), sqlCnn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmdRead;
            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet);
            return dataSet;
        }

        private void AddAuditRecord(string insertStatement)
        {
            if (string.IsNullOrEmpty(insertStatement))
                return;

            SqlConnection sqlCnn = GetSqlConnection();

            SqlCommand cmdInsert = new SqlCommand(insertStatement, sqlCnn);

            cmdInsert.ExecuteNonQuery();
        }
    }
}