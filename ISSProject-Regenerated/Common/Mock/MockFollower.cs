using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mock
{
    internal class MockFollower : IKeyedEntity<int>
    {

        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private int _userId;
        public int UserId { get { return _userId; } set { _userId = value; } }

        private int _followedUserId;
        public int FollowedUserId { get { return _followedUserId; } set { _followedUserId = value; } }

        public MockFollower(int id, int userId, int followedUserId) {
            _id = id;
            _userId = userId;
            _followedUserId = followedUserId;
        }

        public int GetId()
        {
            return _id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
