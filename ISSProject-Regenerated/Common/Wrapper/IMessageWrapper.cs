using ISSProject.Common.Mock;

namespace ISSProject.Common.Wrapper
{
    internal interface IMessageWrapper
    {
        object Clone();
        MockMessage FetchUsingId(int id);
        int GetId();
        MockMessage GetPureReference();
        int GetReceiverId();
        int GetSenderId();
    }
}