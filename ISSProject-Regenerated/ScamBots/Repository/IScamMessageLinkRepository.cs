using ISSProject.Common;
using ISSProject.ScamBots.Model;

namespace ISSProject.ScamBots
{
    internal interface IScamMessageLinkRepository : ISizedRepository<ScamMessageLink, int>
    {
        IEnumerable<ScamMessageLink> All();
        ScamMessageLink ById(int id);
        bool Delete(ScamMessageLink entity);
        string GetMessageLinkRandomly();
        bool Insert(ScamMessageLink entity);
        int LinkIdByUrl(string linkUrl);
        int Size();
        bool Update(ScamMessageLink entity);
    }
}