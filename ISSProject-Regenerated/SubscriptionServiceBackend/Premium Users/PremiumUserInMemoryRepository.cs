using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Wrapper;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users
{
    internal class PremiumUserInMemoryRepository : IPremiumUserRepository
    {
        private List<UserWrapper> users = new List<UserWrapper>();
        public IEnumerable<UserWrapper> All()
        {
            return users;
        }

        public UserWrapper ById(int id)
        {
            int index = users.FindIndex(user => user.GetId() == id);
            if (index == -1)
            {
                return null;
            }
            return users[index];
        }

        public bool Delete(UserWrapper entity)
        {
            int index = users.FindIndex(user => user.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            users.RemoveAt(index);
            return true;
        }

        public bool Insert(UserWrapper entity)
        {
            users.Add(entity);
            return true;
        }

        public bool Update(UserWrapper entity)
        {
            int index = users.FindIndex(user => user.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            users[index] = entity;
            return true;
        }
    }
}
