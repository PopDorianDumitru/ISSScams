using System;
using System.Collections.Generic;
using System.Linq;

namespace ISSProject.Common.Mock
{
    internal class MockUserInMemoryRepository : IMockUserRepository
    {
        private readonly Dictionary<int, MockUser> mockDatabase;

        public MockUserInMemoryRepository()
        {
            mockDatabase = new Dictionary<int, MockUser>();
        }

        public IEnumerable<MockUser> All()
        {
            return mockDatabase.Values.ToList();
        }

        public MockUser ById(int id)
        {
            if (mockDatabase.ContainsKey(id))
            {
                return mockDatabase[id];
            }
            else
            {
                throw new UserRepositoryException("user with id does not exist");
            }
        }

        public bool Delete(MockUser entity)
        {
            return mockDatabase.Remove(entity.Id);
        }

        public bool Insert(MockUser entity)
        {
            if (!mockDatabase.ContainsKey(entity.Id))
            {
                mockDatabase.Add(entity.Id, entity);
                return true;
            }
            else
            {
                throw new UserRepositoryException("same id already exists.");
            }
        }

        public bool Update(MockUser entity)
        {
            if (mockDatabase.ContainsKey(entity.Id))
            {
                mockDatabase[entity.Id] = entity;
                return true;
            }
            else
            {
                throw new UserRepositoryException("user with id does not exist.");
            }
        }

        public int Size()
        {
            return mockDatabase.Count;
        }

        public int GetUserIdByEmail(string email)
        {
            foreach (var user in mockDatabase.Values)
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
