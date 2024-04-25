using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Post
{
    internal class PremiumPostInMemoryRepository : IPremiumPostRepository
    {
        private List<PostWrapper> premiumPosts = new List<PostWrapper>();
        public IEnumerable<PostWrapper> All()
        {
            return premiumPosts;
        }

        public PostWrapper ById(int id)
        {
            int index = premiumPosts.FindIndex(post => post.GetId() == id);
            if (index == -1)
            {
                return null;
            }
            return premiumPosts[index];
        }
        public bool Delete(PostWrapper entity)
        {
            int index = premiumPosts.FindIndex(post => post.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            premiumPosts.RemoveAt(index);
            return true;
        }

        public bool Insert(PostWrapper entity)
        {
            premiumPosts.Add(entity);
            return true;
        }

        public bool Update(PostWrapper entity)
        {
            int index = premiumPosts.FindIndex(post => post.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            premiumPosts[index] = entity;
            return true;
        }
    }
}
