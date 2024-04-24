using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Repository
{
    internal interface IFollowerRepository : ISizedRepository<FollowerWrapper, int>
    {
        IEnumerable<FollowerWrapper> All();
        FollowerWrapper ById(int id);
        bool Delete(FollowerWrapper entity);
        IEnumerable<UserWrapper> FollowersOf(UserWrapper user);
        bool Insert(FollowerWrapper entity);
        int Size();
        bool Update(FollowerWrapper entity);
    }
}