﻿namespace TestManagement.Core.Dapper.Interfaces
{
    /// <summary>
    /// Updates an etity.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IUpdate<TEntity>
      where TEntity : class
    {
        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);
    }
}
