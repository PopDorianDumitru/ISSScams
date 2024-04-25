using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject_Regenerated.Common.Mock;

namespace ISSProject.Common.Mock
{
    internal class MockFollowerRepository : IMockFollowerRepository
    {
        /* Mock Holding Data */
        private static Dictionary<int, MockFollower> mockDatabase = new Dictionary<int, MockFollower>();

        public static void ResetMockDatabase()
        {
            mockDatabase = new Dictionary<int, MockFollower>();

            IEnumerable<MockUser> users = MockUserRepository.Provided().All();
            Random random = new Random();
            int mockKey = 1;
            foreach (var user in users)
            {
                for (int i = 0; i < random.Next(25) + 1; i++)
                {
                    MockUser anotherUser;
                    do
                    {
                        var randomIndex = random.Next(users.Count());
                        anotherUser = users.ToList()[randomIndex];
                    }
                    while (anotherUser.Id == user.Id);
                    mockDatabase.Add(mockKey++, new MockFollower(mockKey, user.Id, anotherUser.Id));
                }
            }
        }

        private static MockFollowerRepository? singleton;

        internal static MockFollowerRepository Singleton { get => singleton; set => singleton = value; }

        public static MockFollowerRepository Provided()
        {
            if (Singleton == null)
            {
                Singleton = new MockFollowerRepository();
            }

            return Singleton;
        }

        /* IRepository */

        public IEnumerable<MockFollower> All()
        {
            return mockDatabase.Values;
        }

        public MockFollower ById(int id)
        {
            return mockDatabase[id];
        }

        public bool Delete(MockFollower entity)
        {
            return mockDatabase.Remove(entity.Id);
        }

        public bool Insert(MockFollower entity)
        {
            if (!mockDatabase.ContainsKey(entity.Id))
            {
                mockDatabase.Add(entity.Id, entity);
                return true;
            }
            else
            {
                throw new MockKeyConstraintViolation(entity.Id);
            }
        }

        public bool Update(MockFollower entity)
        {
            if (mockDatabase.ContainsKey(entity.Id))
            {
                mockDatabase[entity.Id] = entity;
                return true;
            }
            else
            {
                throw new MockNoEntityViolation(entity.Id);
            }
        }

        // Extra
        public IEnumerable<int> FollowersOf(int userId)
        {
            return from follower in mockDatabase.Values
                   where follower.FollowedUserId == userId
                   select follower.UserId;
        }
    }
}
