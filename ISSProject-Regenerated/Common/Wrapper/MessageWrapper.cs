using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace ISSProject.Common.Wrapper
{
    internal class MessageWrapper : IDomainEntityWrapper<MockMessage, int>, IKeyedEntity<int>, IMessageWrapper
    {
        private MockMessage message;

        public MessageWrapper(MockMessage message)
        {
            this.message = message;
        }

        public MessageWrapper(int id, int senderId, int receiverId, string message, DateTime timestamp)
        {
            this.message = new MockMessage(id, senderId, receiverId, message, timestamp);
        }

        public MessageWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockMessage GetPureReference()
        {
            return message;
        }
        public MockMessage FetchUsingId(int id)
        {
            if (message == null)
            {
                message = MockMessageRepository.Provided().ById(id);
            }

            return message;
        }

        // Keyed Entity requirements
        public int GetId()
        {
            return message.Id;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        // Anything else we need here...
        public int GetSenderId()
        {
            return message.SenderId;
        }
        public int GetReceiverId()
        {
            return message.ReceiverId;
        }
    }
}
