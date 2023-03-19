using System;
using System.Collections.Generic;
using System.Data;

namespace Perm.Database.Audit
{
    public class DeleteHandler : TriggerHandlerBase
    {
        protected override string GetTriggerDataQuery()
        {
            return DeletedQuery;
        }

        public override List<AuditModel> GetAuditModels(DataSet dataSet)
        {
            List<AuditModel> auditTails = new List<AuditModel>();
            DataRow oldRecord = dataSet.Tables[0].Rows[0];

            string primaryKeyColumn = oldRecord.Table.Columns[0].ColumnName;
            long.TryParse(oldRecord[primaryKeyColumn].ToString(), out long recordID);

            string tableName = primaryKeyColumn.Remove(primaryKeyColumn.Length - 2, 2);

            foreach (DataColumn newRecordColumn in oldRecord.Table.Columns)
            {
                string columnName = newRecordColumn.ColumnName;
                string oldValue = oldRecord.FormattedValue(columnName);

                auditTails.Add(new AuditModel
                {
                    ChangeOn = DateTime.Now,
                    ColumnName = columnName,
                    EventID = 3,
                    OldValue = oldValue,
                    NewValue = null,
                    TableName = tableName,
                    RecordID = recordID
                });
            }
            return auditTails;
        }
    }
}