using ISSfixed.ISSProject.Mikha.Controllers;
using ISSProject.Common.Mikha.Groups;
using ISSProject.Common.Mikha.Premium_Users;
using ISSProject.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mikha.Controllers
{
    internal class GroupController
    {
        private PremiumUserRepository premiumUserRepository;
        private MockGroupRepository groupRepository;

        public GroupController(PremiumUserRepository premiumUserRepository, MockGroupRepository groupRepository)
        {
            this.premiumUserRepository = premiumUserRepository;
            this.groupRepository = groupRepository;
        }

        public MockGroup GetGroup(int id) { return groupRepository.ById(id); }

        static bool MatchesFilter(MockGroup group, string filter)
        {
            if(group == null) return false; 
            if(filter == null) return false; 
            if(group.GroupName.Contains(filter))
                return true;
            return false;
        }

        public List<MockGroup> ExecuteSearch(UserWrapper searcher, string filter) { 
            if(premiumUserRepository.ById(searcher.GetId()) != null)
            {
                IEnumerable<MockGroup> groups = groupRepository.All();
                if (filter != "")
                {
                    List<MockGroup> filteredGroups = (List<MockGroup>)(from g in groups where MatchesFilter(g, filter) select g);
                    return filteredGroups;
                }
                else
                    return groups.ToList();

            }
            else
            {
                IEnumerable<MockGroup> groups = groupRepository.All();
                groups = groups.Where(g => g.IsPrivate == false);
                if (filter != "")
                {
                    List<MockGroup> filteredGroups = (List<MockGroup>)(from g in groups where MatchesFilter(g, filter) select g);
                    return filteredGroups;
                }
                else
                    return groups.ToList();
            }
            throw new GroupControllerError();    
        }
    }
}
