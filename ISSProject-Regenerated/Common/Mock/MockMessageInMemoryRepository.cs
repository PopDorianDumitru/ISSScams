using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace ISSProject_Regenerated.Common.Mock
{
    internal class MockMessageInMemoryRepository : IMockMessageRepository
    {
        private readonly List<MockMessage> messages;
        public MockMessageInMemoryRepository()
        {
            messages = new List<MockMessage>();
        }

        public IEnumerable<MockMessage> All()
        {
            return messages;
        }

        public MockMessage ById(int id)
        {
            MockMessage message = messages.Find(x => x.Id == id);
            if (message == null)
            {
                throw new MessageRepositoryException("An error occured while trying to get message from the database: a message with specified id does not exist.");
            }
            return message;
        }

        public bool Delete(MockMessage entity)
        {
            return messages.Remove(entity);
        }

        public List<int> GetMessages(int senderId, int receiverId)
        {
            List<int> messagesToFind = new List<int>();

            foreach (MockMessage message in messages)
            {
                if (message.SenderId == senderId && message.ReceiverId == receiverId)
                {
                    messagesToFind.Add(message.Id);
                }
            }

            return messagesToFind;
        }

        public List<int> GetReceiversOfSender(int senderId)
        {
            List<int> receivers = new List<int>();

            foreach (MockMessage message in messages)
            {
                if (message.SenderId == senderId)
                {
                    receivers.Add(message.Id);
                }
            }

            return receivers;
        }

        public bool Insert(MockMessage entity)
        {
            messages.Add(entity);
            return true;
        }

        public int Size()
        {
            return messages.Count;
        }

        public bool Update(MockMessage entity)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Id == entity.Id)
                {
                    messages[i] = entity;
                    return true;
                }
            }
            return false;
        }
    }
}
