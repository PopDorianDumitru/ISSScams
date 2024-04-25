using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Mikha.Groups;
using ISSProject.Common.Mikha.Premium_Messages;
using ISSProject.Common.Wrapper;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages
{
    internal class PremiumMessageInMemoryRepository : IPremiumMessageRepository
    {
        private List<MessageWrapper> messages;

        public PremiumMessageInMemoryRepository()
        {
            messages = new List<MessageWrapper>();
        }
        public IEnumerable<MessageWrapper> All()
        {
            return messages;
        }

        public MessageWrapper ById(int id)
        {
            int messageIndex = messages.FindIndex(m => m.GetId() == id);
            if (messageIndex == -1)
            {
                throw new GroupRepositoryError("An error occured while trying to get group from the database: a group with specified id does not exist.");
            }
            return messages[messageIndex];
        }

        public bool Delete(MessageWrapper entity)
        {
            int messageIndex = messages.FindIndex(m => m.GetId() == entity.GetId());
            if (messageIndex == -1)
            {
                return false;
            }
            messages.RemoveAt(messageIndex);
            return true;
        }

        public bool Insert(MessageWrapper entity)
        {
            messages.Add(entity);
            return true;
        }

        public bool Update(MessageWrapper entity)
        {
            int index = messages.FindIndex(m => m.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            messages[index] = entity;
            return true;
        }
    }
}
