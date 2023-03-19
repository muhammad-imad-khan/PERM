using System;
using System.Collections.Generic;
using System.Data;

namespace Perm.Database.Audit
{

    public class AddHandler : TriggerHandlerBase
    {
        protected override string GetTriggerDataQuery()
        {
            return InsertedQuery;
        }

        public override List<AuditModel> GetAuditModels(DataSet dataSet)
        {
            List<AuditModel> auditTails = new List<AuditModel>();
            DataRow newRecord = dataSet.Tables[0].Rows[0];

            string primaryKeyColumn = newRecord.Table.Columns[0].ColumnName;
            long.TryParse(newRecord[primaryKeyColumn].ToString(), out long recordID);

            string tableName = primaryKeyColumn.Remove(primaryKeyColumn.Length - 2, 2);

            foreach (DataColumn newRecordColumn in newRecord.Table.Columns)
            {
                string columnName = newRecordColumn.ColumnName;
                string newValue = newRecord.FormattedValue(columnName);
                if (!string.IsNullOrEmpty(newValue))
                {
                    auditTails.Add(new AuditModel
                    {
                        ChangeOn = DateTime.Now,
                        ColumnName = columnName,
                        EventID = 1,
                        OldValue = null,
                        NewValue = newValue,
                        TableName = tableName,
                        RecordID = recordID
                    });
                }
            }

            return auditTails;
        }
    }
}