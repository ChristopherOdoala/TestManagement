using System.Collections.Generic;

namespace TestManagement.Core.Dapper.Interfaces
{
    /// <summary>
    /// Finds a list of entites.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IFind<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Get a list of entities
        /// </summary>
        /// <returns>Query result</returns>
        IEnumerable<TEntity> Find();
    }
}
