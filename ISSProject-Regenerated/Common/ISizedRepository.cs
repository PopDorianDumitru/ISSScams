using ISSProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISSProject.Common
{
    internal interface ISizedRepository<T, K> : IRepository<T, K> where T : IKeyedEntity<K>
    {
        /// <summary>
        /// Returns the number of elements currently in the repository.
        /// </summary>
        /// <returns>the number of elements in the repository</returns>
        int Size();
    }
}
