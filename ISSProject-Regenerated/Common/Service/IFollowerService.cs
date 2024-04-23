using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Service
{
    internal interface IFollowerService
    {
        private static IFollowerService? singleton;
        public abstract static IFollowerService Provided();

        /// <summary>
        /// Returns the followers that the provided user has.
        /// </summary>
        /// <param name="user">The wrapped user</param>
        /// <returns>The followers of the wrapped user</returns>
        public IEnumerable<UserWrapper> GetFollowers(UserWrapper user);
    }
}
