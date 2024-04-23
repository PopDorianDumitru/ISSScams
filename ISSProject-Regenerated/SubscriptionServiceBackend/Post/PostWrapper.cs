using ISSProject.Common.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mikha
{
    internal class PostWrapper : IDomainEntityWrapper<MockPost, int>, IKeyedEntity<int>
    {
        private MockPost _post;

        public PostWrapper(MockPost post)
        {
            _post = post;
        }

        public PostWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockPost GetPureReference() { return _post; }
        public MockPost FetchUsingId(int id)
        {
            if (_post == null) _post = MockPostRepository.Provided().ById(id);
            return _post;
        }

        // Keyed Entity requirements
        public int GetId() { return _post.Id; }
        public object Clone() { return MemberwiseClone(); }

        // Anything else we need here...
        public int GetUserId() { return _post.PosterId; }
    }
}
