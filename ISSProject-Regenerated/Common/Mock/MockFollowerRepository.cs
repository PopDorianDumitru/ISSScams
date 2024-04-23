using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mock
{
    internal class MockFollowerRepository : IRepository<MockFollower, int>
    {
        /* Mock Holding Data */
        private static Dictionary<int, MockFollower> _mockDatabase = new Dictionary<int, MockFollower>();

        public static void ResetMockDatabase()
        {
            _mockDatabase = new Dictionary<int, MockFollower>();

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
                    } while (anotherUser.Id == user.Id);
                    _mockDatabase.Add(mockKey++, new MockFollower(mockKey, user.Id, anotherUser.Id));
                }
            }
        }

        private static MockFollowerRepository _singleton;
        public static MockFollowerRepository Provided()
        {
            if (_singleton == null) _singleton = new MockFollowerRepository();
            return _singleton;
        }

        /* IRepository */

        public IEnumerable<MockFollower> All()
        {
            return _mockDatabase.Values;
        }

        public MockFollower ById(int id)
        {
            return _mockDatabase[id];
        }

        public bool Delete(MockFollower entity)
        {
            return _mockDatabase.Remove(entity.Id);
        }

        public bool Insert(MockFollower entity)
        {
            if (!_mockDatabase.ContainsKey(entity.Id))
            {
                _mockDatabase.Add(entity.Id, entity);
                return true;
            }
            else
                throw new MockKeyConstraintViolation(entity.Id);
        }

        public bool Update(MockFollower entity)
        {
            if (!_mockDatabase.ContainsKey(entity.Id))
            {
                _mockDatabase.Add(entity.Id, entity);
                return true;
            }
            else
                throw new MockNoEntityViolation(entity.Id);
        }

        // Extra

        public IEnumerable<int> FollowersOf(int userId)
        {
            return from follower in _mockDatabase.Values
                   where follower.FollowedUserId == userId
                   select follower.UserId;
        }
    }
}
