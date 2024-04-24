
namespace ISSProject.Common.Mikha
{
    internal interface IPremiumPostRepository
    {
        IEnumerable<PostWrapper> All();
        PostWrapper ById(int id);
        bool Delete(PostWrapper entity);
        bool Insert(PostWrapper entity);
        bool Update(PostWrapper entity);
    }
}