using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mikha.Groups
{
    internal class MockGroup : IKeyedEntity<int>
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _group_name;
        public string GroupName { get { return _group_name; } set { _group_name = value; } }

        private bool _is_private;
        public bool IsPrivate { get { return _is_private; } set { _is_private = value; } }

        private List<int> _members_id;
        public List<int> MembersID { get { return _members_id; } set { _members_id = value; } }

        public MockGroup(int id, string group_name, bool is_private)
        {
            _id = id;
            _group_name = group_name;
            _is_private = is_private;
        }

        public int GetId()
        {
            return _id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool addMember(int member_id)
        {
            if(_members_id.Contains(member_id))
                return false;
            _members_id.Add(member_id); 
            return true;
        }
    }
}
