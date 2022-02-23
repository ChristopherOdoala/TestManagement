﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestManagement.Core.Dapper.Interfaces
{
    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IDeleteManyAsync<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Delete a list of existing entities
        /// </summary>
        /// <param name="entities">Entity list</param>
        /// <returns>Task</returns>
        Task DeleteManyAsync(IEnumerable<TEntity> entities);
    }
}
