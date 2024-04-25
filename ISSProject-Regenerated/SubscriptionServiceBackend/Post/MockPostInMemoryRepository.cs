using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Post
{
    internal class MockPostInMemoryRepository : IMockPostRepository
    {
        private List<MockPost> mockPosts = new List<MockPost>();
        public IEnumerable<MockPost> All()
        {
            return mockPosts;
        }

        public MockPost ById(int id)
        {
            int index = mockPosts.FindIndex(post => post.GetId() == id);
            if (index == -1)
            {
                return null;
            }
            return mockPosts[index];
        }

        public bool Delete(MockPost entity)
        {
            int index = mockPosts.FindIndex(post => post.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            mockPosts.RemoveAt(index);
            return true;
        }

        public bool Insert(MockPost entity)
        {
            mockPosts.Add(entity);
            return true;
        }

        public bool Update(MockPost entity)
        {
            int index = mockPosts.FindIndex(post => post.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            mockPosts[index] = entity;
            return true;
        }
    }
}
