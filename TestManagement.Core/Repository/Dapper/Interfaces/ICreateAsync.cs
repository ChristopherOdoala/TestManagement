using System.Threading.Tasks;

namespace TestManagement.Core.Dapper.Interfaces
{
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface ICreateAsync<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Task</returns>
        Task CreateAsync(TEntity entity);
    }
}
