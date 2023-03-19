using System;
using System.Collections.Generic;
using System.Data;
using Perm.Database.Audit;

public static class Helper
{
    public static string FormattedValue(this DataRow dataRow, string columnName)
    {
        if (dataRow.Table.Columns[columnName].DataType == typeof(DateTime) && dataRow[columnName] is DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("o");
        }

        return dataRow[columnName].ToString();
    }

    public static string GenerateScript(this List<AuditModel> auditList)
    {
        string query = "";

        foreach (AuditModel audit in auditList)
        {
            query = query + Environment.NewLine + "--------------------- COLUMN NAME: " + audit.ColumnName + "-------------------------------" + Environment.NewLine;

            query += $"""
                EXEC Admin.Add_Audit @TableName = N'{audit.TableName}',
                                          @RecordID = {audit.RecordID},
                                          @ColumnName = N'{audit.ColumnName}',
                                          @EventID = {audit.EventID},
                                          @NewValue = N'{audit.NewValue}',
                                          @OldValue = N'{audit.OldValue}';
                """;

            query = query + Environment.NewLine + "-----------------------------------------------------------------------------------";
        }

        return query;
    }
}