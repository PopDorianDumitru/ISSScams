using ISSProject.Common.Mock;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Service
{
    internal class FollowerService
    {
        private static FollowerService _singleton;
        public static FollowerService Provided()
        {
            if (_singleton == null) _singleton = new FollowerService();
            return _singleton;
        }

        /// <summary>
        /// Returns the followers that the provided user has.
        /// </summary>
        /// <param name="user">The wrapped user</param>
        /// <returns>The followers of the wrapped user</returns>
        public IEnumerable<UserWrapper> GetFollowers(UserWrapper user)
        {
            return FollowerRepository.Provided().FollowersOf(user);
        }

    }
}
