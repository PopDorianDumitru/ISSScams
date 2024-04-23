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
    internal class FollowerService : IFollowerService
    {
        private static FollowerService? singleton;
        public static IFollowerService Provided()
        {
            if (singleton == null)
            {
                singleton = new FollowerService();
            }

            return singleton;
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
