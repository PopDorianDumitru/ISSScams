using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Cache;

namespace ISSProject.Common
{
    internal abstract class ICachedRepository<T, K> : IRepository<T, K>
        where T : IKeyedEntity<K>
    {
        protected SimpleKeyedMapCache<T, K> cache = new SimpleKeyedMapCache<T, K>();

        public abstract IEnumerable<T> All();
        public abstract T ById(K id);
        public abstract bool Delete(T entity);
        public abstract bool Insert(T entity);
        public abstract int Size();
        public abstract bool Update(T entity);
    }
}
