using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Repository
{
    internal interface IMessageRepository : ISizedRepository<MessageWrapper, int>
    {
        IEnumerable<MessageWrapper> All();
        MessageWrapper ById(int id);
        bool Delete(MessageWrapper entity);
        IEnumerable<MessageWrapper> GetMessages(UserWrapper sender, UserWrapper receiver);
        IEnumerable<UserWrapper> GetReceiversOfSender(UserWrapper sender);
        bool Insert(MessageWrapper entity);
        int Size();
        bool Update(MessageWrapper entity);
    }
}