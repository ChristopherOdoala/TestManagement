using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestManagement.Core.Dapper.Interfaces
{
    /// <summary>
    /// Repository that uses the Dapper micro-ORM framework, see https://github.com/StackExchange/Dapper
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IDapperRepository<TEntity> :
      ISortableRepository<TEntity>,
      IPageableRepository<TEntity>,
      IFindSql<TEntity>,
      IFindSqlAsync<TEntity>
      where TEntity : class
    {
        Task ExecuteMultiResultStoredProcedure(string sql, DynamicParameters parameters, Action<SqlMapper.GridReader> callback);
        Task<IEnumerable<DTO>> ExecuteStoredProcedure<DTO>(string sql, DynamicParameters parameters);
        Task<IEnumerable<DTO>> ExecuteStoredqueryProcedure<DTO>(string sql, DynamicParameters parameters);
        IDbConnection Connection { get; }

        /// <summary>
        /// Use paging
        /// </summary>
        /// <param name="pageNumber">Page to get (one based index).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> Page(int pageNumber, int pageSize);

        /// <summary>
        /// Clear paging
        /// </summary>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> ClearPaging();

        /// <summary>
        /// Sort ascending by a property
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> SortBy(string propertyName);

        /// <summary>
        /// Sort descending by a property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> SortByDescending(string propertyName);

        /// <summary>
        /// Property to sort by (ascending)
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> SortBy(Expression<Func<TEntity, object>> property);

        /// <summary>
        /// Property to sort by (descending)
        /// </summary>
        /// <param name="property">The property</param>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> SortByDescending(Expression<Func<TEntity, object>> property);

        /// <summary>
        /// Clear sorting
        /// </summary>
        /// <returns>Current instance</returns>
        new IDapperRepository<TEntity> ClearSorting();
    }
}
