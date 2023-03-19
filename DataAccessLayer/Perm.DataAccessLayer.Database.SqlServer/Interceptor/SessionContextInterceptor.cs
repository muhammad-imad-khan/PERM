using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Perm.Common;

namespace Perm.DataAccessLayer.Database.SqlServer.Interceptor
{
    public class SessionContextInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionContextInterceptor(IHttpContextAccessor httpContextAccessor)
        {

            _httpContextAccessor = httpContextAccessor;
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await SetDbSession(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task SetDbSession(DbContext dbContext)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                long? userID = _httpContextAccessor.HttpContext.Items["UserID"]?.ParseTo<long>();
                string sessionID = _httpContextAccessor.HttpContext.Items["SessionID"]?.ParseTo<string>();
                string tableName = _httpContextAccessor.HttpContext.Items["RequestTable"]?.ParseTo<string>();
                string entityID = _httpContextAccessor.HttpContext.Items["EntityID"]?.ParseTo<string>();
                if (userID is not null && !string.IsNullOrEmpty(sessionID))
                {
                    SqlParameter keyParameter = new SqlParameter("userKey", "UserID");
                    SqlParameter valueParameter = new SqlParameter("userValue", userID);

                    SqlParameter sessionIDParameter = new SqlParameter("sessionIDKey", "SessionID");
                    SqlParameter sessionValueParameter = new SqlParameter("sessionValueKey", sessionID);

                    SqlParameter tableNameKeyParameter = new SqlParameter("tableName", "TableName");
                    SqlParameter tableNameValueParameter = new SqlParameter("tableNameValue", tableName ?? "");

                    SqlParameter entityIDKeyParameter = new SqlParameter("entityID", "EntityID");
                    SqlParameter entityIDValueParameter = new SqlParameter("entityIDValue", entityID ?? "");
                    if (dbContext != null)
                    {
                        await dbContext.Database.ExecuteSqlRawAsync("EXEC sp_set_session_context @key = @userKey, @value = @userValue;" +
                                                                    "EXEC sp_set_session_context @key=@sessionIDKey, @value=@sessionValueKey;" +
                                                                    "EXEC sp_set_session_context @key=@entityID, @value=@entityIDValue;" +
                                                                    "EXEC sp_set_session_context @key=@tableName, @value=@tableNameValue;",
                            keyParameter,
                            valueParameter,
                            sessionIDParameter,
                            sessionValueParameter,
                            tableNameKeyParameter,
                            tableNameValueParameter,
                            entityIDKeyParameter,
                            entityIDValueParameter);
                    }
                }
            }
        }
    }
}