using ISSProject.Common.Mikha.Groups;
using ISSProject.Common.Wrapper;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Groups
{
    internal interface IGroupController
    {
        List<MockGroup> ExecuteSearch(UserWrapper searcher, string filter);
        MockGroup GetGroup(int id);
    }
}