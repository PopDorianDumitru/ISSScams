using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace ISSProject.MaliciousSubscriptionsBackend.Storage
{
    internal interface ICompanyTokenRepository
    {
        IEnumerable<CompanyToken> All();
        CompanyToken ById(int id);
        bool Delete(CompanyToken entity);
        bool Insert(CompanyToken entity);
        int Size();
        bool Update(CompanyToken entity);
    }
}