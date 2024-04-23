using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mock
{
    internal class MockMessage : IKeyedEntity<int>
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private int _sender_id;
        public int SenderId { get { return _sender_id; } set { _sender_id = value; } }

        private int _receiver_id;
        public int ReceiverId { get { return _receiver_id; } set { _receiver_id = value; } }

        private string _message_content;
        public string MessageContent { get { return _message_content; } set { _message_content = value; } }

        private DateTime _communication_date;
        public DateTime CommunicationDate { get { return _communication_date; } set { _communication_date  = value; } }

        public MockMessage(int id, int sender_id, int receiver_id, string message_content, DateTime communication_date)
        {
            _id = id;
            _sender_id = sender_id;
            _receiver_id = receiver_id;
            _message_content = message_content;
            _communication_date = new SqlDateTime(communication_date).Value;
        }

        public MockMessage(int sender_id, int receiver_id, string message_content, DateTime communication_date)
        {
            _id = -1;
            _sender_id = sender_id;
            _receiver_id = receiver_id;
            _message_content = message_content;
            _communication_date = new SqlDateTime(communication_date).Value;
        }

        public int GetId()
        {
            return _id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
