using System.Linq.Expressions;
using Perm.DataAccessLayer.DataRepository.Core.Model;

namespace Perm.DataAccessLayer.DataRepository.Core;

public static class QueryableExtensions
{
    public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderByDescending(ToLambda<T>(propertyName));
    }

    public static PaginationQueryable<T> ApplyPaginationAndSorting<T>(this IQueryable<T> source, int pageNo, int rowCount, string sortBy, bool isAsc)
    {
        source = source.DefaultIfEmpty();
        return source.ToPaginationQueryable(pageNo, rowCount, sortBy, isAsc);
    }

    public static PaginationQueryable<T> ApplyPaginationAndSorting<T>(this IQueryable<T> source, int pageNo, int rowCount, string sortBy, bool isAsc, bool withDefaultIfEmpty)
    {
        if (withDefaultIfEmpty)
            source = source.DefaultIfEmpty();

        return source.ToPaginationQueryable(pageNo, rowCount, sortBy, isAsc);
    }

    #region Private Methods

    private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
    {
        return source.OrderBy(ToLambda<T>(propertyName));
    }

    private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
    {
        ParameterExpression param = Expression.Parameter(typeof(T), "x");
        Expression body = param;
        foreach (var member in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }

        UnaryExpression propAsObject = Expression.Convert(body, typeof(object));

        Expression<Func<T, object>> expression = Expression.Lambda<Func<T, object>>(propAsObject, param);

        return expression;
    }

    private static PaginationQueryable<T> ToPaginationQueryable<T>(this IQueryable<T> source, int pageNo, int rowCount, string sortBy, bool isAsc)
    {
        IQueryable<T> queryable;
        if (!string.IsNullOrEmpty(sortBy))
            queryable = isAsc ? source.OrderBy(sortBy) : source.OrderByDescending(sortBy);
        else
            queryable = source;

        PaginationQueryable<T> iPaginationQueryable = new()
        {
            OriginalQueryable = queryable,
            SortOrder = isAsc ? "Asc" : "Desc",
            SortBy = sortBy,
            RowSize = rowCount,
            PageNumber = pageNo
        };

        if (rowCount != 0)
        {
            iPaginationQueryable.PagedQueryable = iPaginationQueryable.OriginalQueryable.Skip((pageNo - 1) * rowCount).Take(rowCount);
        }
        else
        {
            iPaginationQueryable.PagedQueryable = queryable;
        }

        return iPaginationQueryable;
    }

    #endregion
}