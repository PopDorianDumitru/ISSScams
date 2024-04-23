using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISSProject.Common;

namespace ISSProject.ScamBots.Model
{
    internal class ScamMessageTemplate : IKeyedEntity<int>
    {
        private int id;
        public int Id
        {
            get { return id; } set { id = value; }
        }

        private string message_content;
        public string MessageContent
        {
            get { return message_content; } set { message_content = value; }
        }

        public ScamMessageTemplate(int id, string message_content)
        {
            this.id = id;
            this.message_content = message_content;
        }
        public ScamMessageTemplate(string message_content)
        {
            id = -1;
            this.message_content = message_content;
        }

        public int GetId()
        {
            return id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
