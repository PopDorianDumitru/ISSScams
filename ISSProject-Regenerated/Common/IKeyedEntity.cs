using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common
{
    /// <summary>
    /// Represents a clonable entity with a key of type K.
    /// </summary>
    /// <typeparam name="K">The type of the key, likely int</typeparam>
    internal interface IKeyedEntity<K> : ICloneable
    {
        /// <summary>
        /// Returns the K-type ID of this Domain Entity.
        /// In general, K will likely be an **integer**.
        /// </summary>
        /// <returns>The ID of this entity</returns>
        K GetId();
    }
}
