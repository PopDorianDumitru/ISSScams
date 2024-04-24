namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal interface ICompanyToken
    {
        object Clone();
        string GetCompanyName();
        int GetId();
        string GetLinkToAPI();
        int GetServiceSeverity();
        string GetToken();
    }
}