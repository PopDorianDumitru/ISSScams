using ISSProject.Common.Mock;

namespace ISSProject.Common.Wrapper
{
    internal interface IUserWrapper
    {
        object Clone();
        MockUser FetchUsingId(int id);
        string GetEmail();
        string GetFirstName();
        int GetId();
        string GetLastName();
        MockUser GetPureReference();
    }
}