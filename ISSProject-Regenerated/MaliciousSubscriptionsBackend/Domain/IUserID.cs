namespace ISSProject.MaliciousSubscriptionsBackend.Domain
{
    internal interface IUserID
    {
        object Clone();
        bool Equals(object? obj);
        int GetHashCode();
        int GetId();
    }
}