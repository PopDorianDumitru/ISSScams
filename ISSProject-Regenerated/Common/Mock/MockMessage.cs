using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace ISSProject.Common.Mock
{
    internal class MockMessage : IKeyedEntity<int>
    {
        private int id;
        public int Id
        {
            get { return id; } set { id = value; }
        }

        private int sender_id;
        public int SenderId
        {
            get { return sender_id; } set { sender_id = value; }
        }

        private int receiver_id;
        public int ReceiverId
        {
            get { return receiver_id; } set { receiver_id = value; }
        }

        private string message_content;
        public string MessageContent
        {
            get { return message_content; } set { message_content = value; }
        }

        private DateTime communication_date;
        public DateTime CommunicationDate
        {
            get { return communication_date; } set { communication_date = value; }
        }

        public MockMessage(int id, int sender_id, int receiver_id, string message_content, DateTime communication_date)
        {
            this.id = id;
            this.sender_id = sender_id;
            this.receiver_id = receiver_id;
            this.message_content = message_content;
            this.communication_date = new SqlDateTime(communication_date).Value;
        }

        public MockMessage(int sender_id, int receiver_id, string message_content, DateTime communication_date)
        {
            id = -1;
            this.sender_id = sender_id;
            this.receiver_id = receiver_id;
            this.message_content = message_content;
            this.communication_date = new SqlDateTime(communication_date).Value;
        }

        public int GetId()
        {
            return id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object @object)
        {
            if (@object == null || GetType() != @object.GetType())
            {
                return false;
            }

            MockMessage other = (MockMessage)@object;

            // Check all fields for equality
            return Id == other.Id &&
                   SenderId == other.SenderId &&
                   ReceiverId == other.ReceiverId &&
                   MessageContent == other.MessageContent &&
                   CommunicationDate == other.CommunicationDate;
        }
    }
}
