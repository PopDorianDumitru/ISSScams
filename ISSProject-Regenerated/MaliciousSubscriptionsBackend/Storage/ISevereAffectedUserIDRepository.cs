using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace ISSProject.MaliciousSubscriptionsBackend.Storage
{
    internal interface ISevereAffectedUserIDRepository
    {
        IEnumerable<UserID> All();
        UserID ById(int id);
        bool Delete(UserID entity);
        bool Insert(UserID entity);
        int Size();
        bool Update(UserID entity);
    }
}