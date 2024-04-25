using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha.Groups;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Groups
{
    internal class MockGroupInMemoryRepository : IMockGroupRepository
    {
        private List<MockGroup> mockGroups = new List<MockGroup>();
        public MockGroupInMemoryRepository()
        {
        }
        public IEnumerable<MockGroup> All()
        {
            return mockGroups;
        }

        public MockGroup ById(int id)
        {
            int index = mockGroups.FindIndex(group => group.GetId() == id);
            if (index == -1)
            {
                return null;
            }
            return mockGroups[index];
        }

        public bool Delete(MockGroup entity)
        {
            int index = mockGroups.FindIndex(group => group.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            mockGroups.RemoveAt(index);
            return true;
        }

        public bool Insert(MockGroup entity)
        {
            mockGroups.Add(entity);
            return true;
        }

        public bool Update(MockGroup entity)
        {
            int index = mockGroups.FindIndex(group => group.GetId() == entity.GetId());
            if (index == -1)
            {
                return false;
            }
            mockGroups[index] = entity;
            return true;
        }
    }
}
