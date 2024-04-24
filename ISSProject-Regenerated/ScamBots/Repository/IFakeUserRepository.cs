using ISSProject.Common;
using ISSProject.Common.Mock;

namespace ISSProject.ScamBots
{
    internal interface IFakeUserRepository : ISizedRepository<MockUser, int>
    {
        IEnumerable<MockUser> All();
        MockUser ById(int id);
        bool Delete(MockUser entity);
        bool Insert(MockUser entity);
        int NumberOfBannedFakeAccounts();
        int Size();
        bool Update(MockUser entity);
        int UserIdByEmail(string email);
    }
}