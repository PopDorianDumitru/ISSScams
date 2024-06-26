﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Mikha.Controllers;
using ISSProject.Common.Mikha.Groups;
using ISSProject.Common.Mikha.Premium_Users;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Groups
{
    internal class GroupController : IGroupController
    {
        private IPremiumUserRepository premiumUserRepository;
        private IMockGroupRepository groupRepository;

        public GroupController(IPremiumUserRepository premiumUserRepository, IMockGroupRepository groupRepository)
        {
            this.premiumUserRepository = premiumUserRepository;
            this.groupRepository = groupRepository;
        }

        public MockGroup GetGroup(int id)
        {
            return groupRepository.ById(id);
        }

        private static bool MatchesFilter(MockGroup group, string filter)
        {
            if (group == null)
            {
                return false;
            }

            if (filter == null)
            {
                return false;
            }

            if (group.GroupName.Contains(filter))
            {
                return true;
            }

            return false;
        }

        public List<MockGroup> ExecuteSearch(UserWrapper searcher, string filter)
        {
            if (premiumUserRepository.ById(searcher.GetId()) != null)
            {
                List<MockGroup> groups = (List<MockGroup>)groupRepository.All();
                if (filter != string.Empty)
                {
                    groups.RemoveAll(group => !MatchesFilter(group, filter));
                    return groups;
                }
                else
                {
                    return groups;
                }
            }
            else
            {
                List<MockGroup> groups = (List<MockGroup>)groupRepository.All();
                groups.RemoveAll(g => g.IsPrivate == true);
                if (filter != string.Empty)
                {
                    groups.RemoveAll(group => !MatchesFilter(group, filter));
                    return groups;
                }
                else
                {
                    return groups;
                }
            }
        }
    }
}
