using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace ISSProject.Common.Mikha
{
    internal class PostWrapper : IDomainEntityWrapper<MockPost, int>, IKeyedEntity<int>
    {
        private MockPost post;
        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                PostWrapper postWrapper = (PostWrapper)obj;
                return post.Equals(postWrapper.GetPureReference());
            }
        }
        public PostWrapper(MockPost post)
        {
            this.post = post;
        }

        public PostWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockPost GetPureReference()
        {
            return post;
        }
        public MockPost FetchUsingId(int id)
        {
            if (post == null)
            {
                post = MockPostRepository.Provided().ById(id);
            }

            return post;
        }

        // Keyed Entity requirements
        public int GetId()
        {
            return post.Id;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        // Anything else we need here...
        public int GetUserId()
        {
            return post.PosterId;
        }
    }
}
