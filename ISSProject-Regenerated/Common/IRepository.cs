using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common
{
    /// <summary>
    /// A repository common interface.
    /// LOAD the implementation in the constructor if it is a FILE-BASED one.
    /// SAVE the implementation in each modifying operation if it is a FILE-BASED one.
    /// </summary>
    /// <typeparam name="T">The entity this repository manages</typeparam>
    /// <typeparam name="K">The ID of the entity in this repository</typeparam>
    internal interface IRepository<T, K> where T : IKeyedEntity<K>
    {

        /// <summary>
        /// Gets all T objects from this repository in any order.
        /// </summary>
        /// <returns>All T objects from repo.</returns>
        IEnumerable<T> All();

        /// <summary>
        /// Gets object T based on id K from this repository.
        /// </summary>
        /// <param name="id">The ID of the entity</param>
        /// <returns>The entity whose ID is K</returns>
        T ById(K id);

        /// <summary>
        /// Inserts a new entity T into this repository.
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <returns>True if inserted, False otherwise</returns>
        bool Insert(T entity);

        /// <summary>
        /// Updates an entity in this repository.
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>True if updated, False otherwise</returns>
        bool Update(T entity);

        /// <summary>
        /// Deletes an entity in this repository.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>True if deleted, False otherwise</returns>
        bool Delete(T entity);
    }
}
