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

    public static string GenerateScript(this List<AuditTrailModel> auditTrails)
    {
        string query = "";

        foreach (AuditTrailModel auditTrail in auditTrails)
        {
            query = query + Environment.NewLine + "--------------------- COLUMN NAME: " + auditTrail.ColumnName + "-------------------------------" + Environment.NewLine;

            query += $"""
                EXEC Admin.Add_AuditTrail @TableName = N'{auditTrail.TableName}',
                                          @RecordID = {auditTrail.RecordID},
                                          @ColumnName = N'{auditTrail.ColumnName}',
                                          @EventID = {auditTrail.EventID},
                                          @NewValue = N'{auditTrail.NewValue}',
                                          @OldValue = N'{auditTrail.OldValue}';
                """;

            query = query + Environment.NewLine + "-----------------------------------------------------------------------------------";
        }

        return query;
    }
}