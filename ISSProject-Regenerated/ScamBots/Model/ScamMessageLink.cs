using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISSProject.Common;

namespace ISSProject.ScamBots.Model
{
    internal class ScamMessageLink : IKeyedEntity<int>
    {
        private int id;
        public int Id
        {
            get { return id; } set { id = value; }
        }

        private string link_url;
        public string LinkUrl
        {
            get { return link_url; } set { link_url = value; }
        }

        public ScamMessageLink(int id, string linkUrl)
        {
            this.id = id;
            link_url = linkUrl;
        }

        public ScamMessageLink(string linkUrl)
        {
            id = -1;
            link_url = linkUrl;
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
