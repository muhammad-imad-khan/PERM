using Microsoft.EntityFrameworkCore;

namespace Perm.DataAccessLayer.Database.SqlServer;

/// <summary>
/// 
/// </summary>
public interface IEntityResolver
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    void SetUp(ModelBuilder modelBuilder);
}