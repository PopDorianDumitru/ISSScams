using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject_Regenerated.Common.Cache;

namespace ISSProject.Common.Cache
{
    /// <summary>
    /// A generic cache implementation based on a Dictionary.
    /// </summary>
    /// <typeparam name="T">The entity this cache contains</typeparam>
    /// <typeparam name="K">The ID of the entity in this cache</typeparam>
    internal class SimpleKeyedMapCache<T, K> : ISimpleKeyedMapCache<T, K>
        where T : IKeyedEntity<K>
    {
        private Dictionary<K, T> cacheContent = new Dictionary<K, T>();

        /// <summary>
        /// Returns the cached entity T by its id K.
        /// </summary>
        /// <param name="id">The ID of the entity T</param>
        /// <returns>The entity T, if cached. Otherwise will throw an error.
        /// Check if cached with cache.Any(id) first.</returns>
        public T ById(K id)
        {
            return cacheContent[id];
        }

        /// <summary>
        /// Returns the content of the cache.
        /// </summary>
        /// <returns> The cache as a Dictionary with keys of type K and values of type T. </returns>
        public Dictionary<K, T> GetCache()
        {
            return cacheContent;
        }

        /// <summary>
        /// Checks if the entity by id K is cached or not.
        /// </summary>
        /// <param name="id">The ID of the entity T</param>
        /// <returns>True if cached, False otherwise</returns>
        public bool Any(K id)
        {
            return cacheContent.ContainsKey(id);
        }

        /// <summary>
        /// Adds the entity to this cache if not inside already.
        /// If you wish to update an entity instead of adding it, use cache.Update(entity) instead.
        /// </summary>
        /// <param name="entity">The entity to added</param>
        /// <returns>True if added, False if already in cache</returns>
        public bool Add(T entity)
        {
            if (!Any(entity.GetId()))
            {
                cacheContent.Add(entity.GetId(), (T)entity.Clone());
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes an entity from this cache. Likely not to be used.
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        /// <returns>True if removed, False if it was not in the cache yet</returns>
        public bool Remove(T entity)
        {
            return cacheContent.Remove(entity.GetId());
        }

        /// <summary>
        /// Updates an entity in this cache.
        /// If you wish to add an entity instead of updating it, use cache.Add(entity) instead.
        /// </summary>
        /// <param name="entity">Your updated entity</param>
        /// <returns>True if the entity was updated, False if it was not yet in the cache.</returns>
        public bool Update(T entity)
        {
            if (Any(entity.GetId()))
            {
                cacheContent[entity.GetId()] = (T)entity.Clone();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
