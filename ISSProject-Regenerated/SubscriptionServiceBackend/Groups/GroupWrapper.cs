using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mikha.Groups
{
    internal class GroupWrapper : IDomainEntityWrapper<MockGroup, int>, IKeyedEntity<int>
    {
        private MockGroup _group;

        public GroupWrapper(MockGroup post)
        {
            _group = post;
        }

        public GroupWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockGroup GetPureReference() { return _group; }
        public MockGroup FetchUsingId(int id)
        {
            if (_group == null) _group = MockGroupRepository.Provided().ById(id);
            return _group;
        }

        // Keyed Entity requirements
        public int GetId() { return _group.Id; }
        public object Clone() { return MemberwiseClone(); }

        // Anything else we need here...
        public List<int> GetUsersIDs()
        {
            return _group.MembersID.ToList();
        }

        public string GetGroupName()
        {
            return _group.GroupName;
        }

        public bool GetGroupVisibility()
        {
            return _group.IsPrivate;
        }
    }
}
