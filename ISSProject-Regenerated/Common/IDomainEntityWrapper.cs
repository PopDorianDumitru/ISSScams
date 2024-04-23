using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common
{
    /// <summary>
    /// Represents a wrapped domain entity, potentially borrowed from another team.
    /// The reasoning behind wrapping is to abstract away the implementation details
    /// enforced by each different team and to normalize it to our own requirements.
    /// </summary>
    /// <typeparam name="T">The pure domain entity this class wraps</typeparam>
    /// <typeparam name="K">The key by which we index this entity</typeparam>
    internal interface IDomainEntityWrapper<T, K>
    {
        /// <summary>
        /// Returns a pure T-type reference for this object.
        /// Essentially, it returns the entity which this class is wrapping.
        /// This is likely not needed in most situations, but please do note
        /// some objects may require pure references.
        /// </summary>
        /// <returns></returns>
        T GetPureReference();

        /// <summary>
        /// Fills the data in the wrapper based on the K-type ID.
        /// The filling is done by whatever mechanism the pure reference requires.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FetchUsingId(K id);
    }
}
