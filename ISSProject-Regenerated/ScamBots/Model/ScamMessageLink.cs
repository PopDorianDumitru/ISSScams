using ISSProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISSProject.ScamBots.Model
{
    internal class ScamMessageLink : IKeyedEntity<int>
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _link_url;
        public string LinkUrl { get { return _link_url; } set { _link_url = value; } }

        public ScamMessageLink(int id, string linkUrl)
        {
            _id = id;
            _link_url = linkUrl;
        }

        public ScamMessageLink(string linkUrl)
        {
            _id = -1;
            _link_url = linkUrl;
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
