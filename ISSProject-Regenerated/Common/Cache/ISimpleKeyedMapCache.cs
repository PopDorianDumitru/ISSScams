using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;

namespace ISSProject_Regenerated.Common.Cache
{
    internal interface ISimpleKeyedMapCache<T, K>
        where T : IKeyedEntity<K>
    {
        /// <summary>
        /// Retrieves an entity from the cache by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        T ById(K id);

        /// <summary>
        /// Retrieves all cached entities as a dictionary.
        /// </summary>
        /// <returns>A dictionary containing all cached entities.</returns>
        Dictionary<K, T> GetCache();

        /// <summary>
        /// Checks if an entity with the specified ID is present in the cache.
        /// </summary>
        /// <param name="id">The ID of the entity to check.</param>
        /// <returns>True if the entity is cached; otherwise, false.</returns>
        bool Any(K id);

        /// <summary>
        /// Adds an entity to the cache if it is not already present.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>True if the entity was added; otherwise, false.</returns>
        bool Add(T entity);

        /// <summary>
        /// Removes an entity from the cache.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>True if the entity was removed; otherwise, false.</returns>
        bool Remove(T entity);

        /// <summary>
        /// Updates an entity in the cache.
        /// </summary>
        /// <param name="entity">The updated entity.</param>
        /// <returns>True if the entity was updated; otherwise, false.</returns>
        bool Update(T entity);
    }
}
