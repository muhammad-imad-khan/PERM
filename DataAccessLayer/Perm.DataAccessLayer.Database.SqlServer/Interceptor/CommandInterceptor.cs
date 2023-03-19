using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Perm.DataAccessLayer.Database.SqlServer.Interceptor;

public class CommandInterceptor : DbCommandInterceptor
{
    public CommandInterceptor() { }

    public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default) =>
        await base.ReaderExecutingAsync(command, eventData, result, cancellationToken);
}