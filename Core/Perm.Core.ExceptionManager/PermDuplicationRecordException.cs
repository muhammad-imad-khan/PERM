using Microsoft.Data.SqlClient;
using Perm.Core.ExceptionManager;

namespace Perm.Core.RequestManager.Processor.PermException
{
    public class PermDuplicateRecordException : PermDatabaseException
    {
        public readonly string IndexName;
        public readonly string Value;

        public PermDuplicateRecordException(string message) : base(message)
        {
        }

        public PermDuplicateRecordException(int code) : base(code)
        {
        }

        public PermDuplicateRecordException(SqlException sqlException) : base(sqlException.Number)
        {
        }

        public PermDuplicateRecordException(SqlException sqlException, string indexName, string value) : base(sqlException.Number)
        {
            IndexName = indexName;
            Value = value;
        }
    }
}