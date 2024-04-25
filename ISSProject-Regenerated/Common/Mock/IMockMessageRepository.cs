namespace ISSProject.Common.Mock
{
    internal interface IMockMessageRepository : ISizedRepository<MockMessage, int>
    {
        IEnumerable<MockMessage> All();
        MockMessage ById(int id);
        bool Delete(MockMessage entity);
        List<int> GetMessages(int senderId, int receiverId);
        List<int> GetReceiversOfSender(int senderId);
        bool Insert(MockMessage entity);
        int Size();
        bool Update(MockMessage entity);
    }
}