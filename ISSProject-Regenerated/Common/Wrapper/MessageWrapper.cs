using ISSProject.Common.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Wrapper
{
    internal class MessageWrapper : IDomainEntityWrapper<MockMessage, int>, IKeyedEntity<int>
    {

        private MockMessage _message;

        public MessageWrapper(MockMessage message)
        {
            _message = message;
        }

        public MessageWrapper(int id, int senderId, int receiverId, string message, DateTime timestamp)
        {
            _message = new MockMessage(id, senderId, receiverId, message, timestamp);
        }

        public MessageWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockMessage GetPureReference() { return _message; }
        public MockMessage FetchUsingId(int id)
        {
            if (_message == null) _message = MockMessageRepository.Provided().ById(id);
            return _message;
        }

        // Keyed Entity requirements
        public int GetId() { return _message.Id; }
        public object Clone() { return MemberwiseClone(); }

        // Anything else we need here...
        public int GetSenderId() { return _message.SenderId; }
        public int GetReceiverId() { return _message.ReceiverId; }

    }
}
