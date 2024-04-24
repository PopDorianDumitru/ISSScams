using ISSProject.Common;
using ISSProject.ScamBots.Model;

namespace ISSProject.ScamBots
{
    internal interface IScamMessageRepository : ISizedRepository<ScamMessageTemplate, int>
    {
        IEnumerable<ScamMessageTemplate> All();
        ScamMessageTemplate ById(int id);
        bool Delete(ScamMessageTemplate entity);
        string GetMessageTemplateRandomly();
        bool Insert(ScamMessageTemplate entity);
        int MessageIdByTemplate(string templateContent);
        int Size();
        bool Update(ScamMessageTemplate entity);
    }
}