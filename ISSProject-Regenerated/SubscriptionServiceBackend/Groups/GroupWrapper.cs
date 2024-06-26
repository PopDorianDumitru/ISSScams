﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace ISSProject.Common.Mikha.Groups
{
    internal class GroupWrapper : IDomainEntityWrapper<MockGroup, int>, IKeyedEntity<int>
    {
        private MockGroup group;
        public override bool Equals(object comparisonObject)
        {
                MockGroup comparisonGroup = (MockGroup)comparisonObject;
                return (this.GetGroupName() == comparisonGroup.GroupName) && (this.GetGroupVisibility() == comparisonGroup.IsPrivate) && (this.GetId() == comparisonGroup.Id);
        }

        public GroupWrapper(MockGroup post)
        {
            group = post;
        }

        public GroupWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockGroup GetPureReference()
        {
            return group;
        }
        public MockGroup FetchUsingId(int id)
        {
            if (group == null)
            {
                group = MockGroupRepository.Provided().ById(id);
            }

            return group;
        }

        // Keyed Entity requirements
        public int GetId()
        {
            return group.Id;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        // Anything else we need here...
        public List<int> GetUsersIDs()
        {
            return group.MembersID.ToList();
        }

        public string GetGroupName()
        {
            return group.GroupName;
        }

        public bool GetGroupVisibility()
        {
            return group.IsPrivate;
        }
    }
}
