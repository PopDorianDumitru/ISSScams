using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mikha.Groups
{
    internal class MockGroup : IKeyedEntity<int>
    {
        private int id;
        public int Id
        {
            get { return id; } set { id = value; }
        }

        private string group_name;
        public string GroupName
        {
            get { return group_name; } set { group_name = value; }
        }

        private bool is_private;
        public bool IsPrivate
        {
            get { return is_private; } set { is_private = value; }
        }

        private List<int> members_id;
        public List<int> MembersID
        {
            get { return members_id; } set { members_id = value; }
        }

        public MockGroup(int id, string group_name, bool is_private)
        {
            this.id = id;
            this.group_name = group_name;
            this.is_private = is_private;
            this.members_id = new List<int>();
        }

        public int GetId()
        {
            return id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool AddMember(int member_id)
        {
            if (members_id.Contains(member_id))
            {
                return false;
            }

            members_id.Add(member_id);
            return true;
        }
    }
}
