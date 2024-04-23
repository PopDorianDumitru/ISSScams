using ISSProject.Common.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Wrapper
{
    internal class FollowerWrapper : IDomainEntityWrapper<MockFollower, int>, IKeyedEntity<int>
    {

        private MockFollower _follower;

        public FollowerWrapper(MockFollower follower)
        {
            _follower = follower;
        }

        public FollowerWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockFollower GetPureReference() { return _follower; }
        public MockFollower FetchUsingId(int id)
        {
            if (_follower == null) _follower = MockFollowerRepository.Provided().ById(id);
            return _follower;
        }

        // Keyed Entity requirements
        public int GetId() { return _follower.Id; }
        public object Clone() { return MemberwiseClone(); }

        // Anything else we need here...
        public int GetUserId() { return _follower.UserId; }
        public int GetFollowerId() { return _follower.FollowedUserId; }

    }
}
