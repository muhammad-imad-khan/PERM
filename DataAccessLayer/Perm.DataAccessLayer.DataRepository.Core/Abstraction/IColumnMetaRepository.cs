using Perm.Model.Config;

namespace Perm.DataAccessLayer.DataRepository.Core.Abstraction
{
    public interface IColumnMetaRepository : IRepository<ColumnMetaModel>
    {
        Task AddColumnMeta(ColumnMetaModel columnMeta);

        void UpdateColumnMeta(ColumnMetaModel columnMeta);

        Task DeleteColumnMeta(long columnMetaID);

        List<ColumnMetaModel> GetColumnMeta(long entityTypeID);
    }
}