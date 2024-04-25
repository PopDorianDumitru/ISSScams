using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mock
{
    internal class MockFollower : IKeyedEntity<int>
    {
        private int id;

        public override bool Equals(object obj)
        {
            // Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                MockFollower mf = (MockFollower)obj;
                return (Id == mf.Id) && (UserId == mf.UserId) && (FollowedUserId == mf.FollowedUserId);
            }
        }

        public int Id
        {
            get { return id; } set { id = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; } set { userId = value; }
        }

        private int followedUserId;
        public int FollowedUserId
        {
            get { return followedUserId; } set { followedUserId = value; }
        }

        public MockFollower(int id, int userId, int followedUserId)
        {
            this.id = id;
            this.userId = userId;
            this.followedUserId = followedUserId;
        }

        public int GetId()
        {
            return id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
