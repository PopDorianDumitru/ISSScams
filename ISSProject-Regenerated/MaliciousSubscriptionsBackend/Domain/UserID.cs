using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;

namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal class UserID : IKeyedEntity<int>, IUserID
    {
        private int userID;

        public UserID(int userID)
        {
            this.userID = userID;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int GetId()
        {
            return userID;
        }

        public override bool Equals(object? obj)
        {
            if (obj is UserID)
            {
                return this.userID == ((UserID)obj).userID;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.userID.GetHashCode();
        }
    }
}
