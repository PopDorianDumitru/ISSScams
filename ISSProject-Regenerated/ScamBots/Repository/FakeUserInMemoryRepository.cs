using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;

namespace ISSProject_Regenerated.ScamBots.Repository
{
    internal class FakeUserInMemoryRepository : IFakeUserRepository
    {
        private readonly List<MockUser> mockUsers = new List<MockUser>();
        public IEnumerable<MockUser> All()
        {
            return mockUsers.ToArray();
        }

        public MockUser ById(int id)
        {
            return mockUsers.Find(user => user.Id == id);
        }

        public bool Delete(MockUser entity)
        {
            return mockUsers.Remove(entity);
        }

        public bool Insert(MockUser entity)
        {
            mockUsers.Add(entity);
            return true;
        }

        public int NumberOfBannedFakeAccounts()
        {
            return 13;
        }

        public int Size()
        {
           return mockUsers.Count;
        }

        public bool Update(MockUser entity)
        {
            for (int i = 0; i < mockUsers.Count; i++)
            {
                if (mockUsers[i].Id == entity.Id)
                {
                    mockUsers[i] = entity;
                    return true;
                }
            }
            return false;
        }
        public int UserIdByEmail(string email)
        {
            foreach (var user in mockUsers)
            {
                if (user.Email == email)
                {
                    return user.Id;
                }
            }
            return -1;
        }
    }
}
