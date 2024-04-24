using ISSProject.Common.Mock;

namespace ISSProject.Common.Wrapper
{
    internal interface IFollowerWrapper
    {
        object Clone();
        MockFollower FetchUsingId(int id);
        int GetFollowerId();
        int GetId();
        MockFollower GetPureReference();
        int GetUserId();
    }
}