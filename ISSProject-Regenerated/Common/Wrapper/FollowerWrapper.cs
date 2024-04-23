using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace ISSProject.Common.Wrapper
{
    internal class FollowerWrapper : IDomainEntityWrapper<MockFollower, int>, IKeyedEntity<int>
    {
        private MockFollower follower;

        public FollowerWrapper(MockFollower follower)
        {
            this.follower = follower;
        }

        public FollowerWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockFollower GetPureReference()
        {
            return follower;
        }
        public MockFollower FetchUsingId(int id)
        {
            if (follower == null)
            {
                follower = MockFollowerRepository.Provided().ById(id);
            }

            return follower;
        }

        // Keyed Entity requirements
        public int GetId()
        {
            return follower.Id;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        // Anything else we need here...
        public int GetUserId()
        {
            return follower.UserId;
        }
        public int GetFollowerId()
        {
            return follower.FollowedUserId;
        }
    }
}
