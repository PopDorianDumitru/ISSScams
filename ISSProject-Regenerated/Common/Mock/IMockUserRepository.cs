namespace ISSProject.Common.Mock
{
    internal interface IMockUserRepository : ISizedRepository<MockUser, int>
    {
        IEnumerable<MockUser> All();
        MockUser ById(int id);
        bool Delete(MockUser entity);
        bool Insert(MockUser entity);
        int Size();
        bool Update(MockUser entity);
    }
}