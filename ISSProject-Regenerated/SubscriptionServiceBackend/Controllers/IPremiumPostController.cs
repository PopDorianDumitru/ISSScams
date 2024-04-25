namespace ISSProject.Common.Mikha.Controllers
{
    internal interface IPremiumPostController
    {
        bool AddPremiumPost(MockPost post);
        bool DeletePremiumPost(MockPost post);
        PriorityQueue<MockPost, int> GetPostQueue();
    }
}