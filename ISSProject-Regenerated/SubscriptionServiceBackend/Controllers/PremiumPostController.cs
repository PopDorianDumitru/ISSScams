using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISSProject.Common.Mikha.Premium_Messages;
using ISSProject.Common.Mikha.Premium_Users;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.SubscriptionServiceBackend.Post;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;

namespace ISSProject.Common.Mikha.Controllers
{
    internal class PremiumPostController : IPremiumPostController
    {
        private IMockPostRepository mockPostRepository;
        private IPremiumPostRepository premiumPostRepository;
        private IPremiumUserRepository premiumUserRepository;

        public PremiumPostController(IMockPostRepository mockPostRepository, IPremiumPostRepository premiumPostRepository, IPremiumUserRepository premiumUserRepository)
        {
            this.mockPostRepository = mockPostRepository;
            this.premiumPostRepository = premiumPostRepository;
            this.premiumUserRepository = premiumUserRepository;
        }

        public bool AddPremiumPost(MockPost post)
        {
            try
            {
                if (premiumUserRepository.ById(post.PosterId) != null)
                {
                    bool insert1Result = mockPostRepository.Insert(post);
                    if (insert1Result)
                    {
                        bool insert2Result = premiumPostRepository.Insert(new PostWrapper(post));
                        if (insert2Result)
                        {
                            return true;
                        }
                        else
                        {
                            premiumPostRepository.Delete(new PostWrapper(post));
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePremiumPost(MockPost post)
        {
            try
            {
                if (premiumUserRepository.ById(post.PosterId) != null)
                {
                    premiumPostRepository.Delete(new PostWrapper(post));
                    // For completeness
                    mockPostRepository.Delete(post);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public PriorityQueue<MockPost, int> GetPostQueue()
        {
            PriorityQueue<MockPost, int> posts = new PriorityQueue<MockPost, int>();
            foreach (MockPost post in mockPostRepository.All())
            {
                if (premiumPostRepository.ById(post.Id) != null)
                {
                    posts.Enqueue(post, 0);
                }
                else
                {
                    posts.Enqueue(post, 1);
                }
            }
            return posts;
        }
    }
}
