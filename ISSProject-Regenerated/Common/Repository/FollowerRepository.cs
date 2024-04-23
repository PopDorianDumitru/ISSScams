using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Repository
{
    internal class FollowerRepository : ICachedRepository<FollowerWrapper, int>, ISizedRepository<FollowerWrapper, int>
    {
        private static FollowerRepository? singleton;
        public static FollowerRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new FollowerRepository();
            }

            return singleton;
        }

        public override IEnumerable<FollowerWrapper> All()
        {
            return from mockFollower in MockFollowerRepository.Provided().All()
                   select new FollowerWrapper(mockFollower);
        }

        public override FollowerWrapper ById(int id)
        {
            if (cache.Any(id))
            {
                return cache.ById(id);
            }

            var user = new FollowerWrapper(id);
            cache.Add(user);
            return user;
        }

        public override bool Delete(FollowerWrapper entity)
        {
            if (cache.Any(entity.GetId()))
            {
                cache.Remove(entity);
            }

            return MockFollowerRepository.Provided().Delete(entity.GetPureReference());
        }

        public override bool Insert(FollowerWrapper entity)
        {
            cache.Add(entity);
            return MockFollowerRepository.Provided().Insert(entity.GetPureReference());
        }

        public override int Size()
        {
            return MockFollowerRepository.Provided().All().Count(); // slow, but unused
        }

        public override bool Update(FollowerWrapper entity)
        {
            cache.Update(entity);
            return MockFollowerRepository.Provided().Update(entity.GetPureReference());
        }

        public IEnumerable<UserWrapper> FollowersOf(UserWrapper user)
        {
            return from followerId in MockFollowerRepository.Provided().FollowersOf(user.GetId())
                   select new UserWrapper(followerId);
        }
    }
}
