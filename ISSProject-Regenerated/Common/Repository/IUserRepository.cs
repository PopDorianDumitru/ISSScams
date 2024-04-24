using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Repository
{
    internal interface IUserRepository : ISizedRepository<UserWrapper, int>
    {
        IEnumerable<UserWrapper> All();
        bool Any(int id);
        UserWrapper ByEmail(string email);
        UserWrapper ById(int id);
        bool Delete(UserWrapper entity);
        bool Insert(UserWrapper entity);
        int Size();
        bool Update(UserWrapper entity);
    }
}