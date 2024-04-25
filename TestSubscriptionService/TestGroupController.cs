using ISSProject_Regenerated.SubscriptionServiceBackend.Groups;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
using System;
using System.Collections.Generic;
using System.Linq;
using ISSProject.Common.Mikha.Groups;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Wrapper;
using System.Windows.Forms;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestGroupController
    {
        IPremiumUserRepository premiumUserRepository;
        IMockGroupRepository groupRepository;
        IGroupController controller;
        [TestInitialize]
        public void TestInitializer()
        {
            premiumUserRepository = new PremiumUserInMemoryRepository();
            int premiumUserId = 1;
            UserWrapper premiumUser = new UserWrapper(premiumUserId);
            premiumUserRepository.Insert(premiumUser);

            groupRepository = new MockGroupInMemoryRepository();
            string firstPublicGroupName = "Group1";
            int firstPublicGroupId = 1;
            bool firstPublicGroupIsPrivate = false;
            MockGroup group = new MockGroup(firstPublicGroupId, firstPublicGroupName, firstPublicGroupIsPrivate);
            groupRepository.Insert(group);

            string secondPublicGroupName = "Group2Filter";
            int secondPublicGroupId = 2;
            bool secondPublicGroupIsPrivate = false;
            MockGroup secondGroup = new MockGroup(secondPublicGroupId, secondPublicGroupName, secondPublicGroupIsPrivate);
            groupRepository.Insert(secondGroup);

            string firstPrivateGroupName = "Group3Filter";
            int firstPrivateGroupId = 3;
            bool firstPrivateGroupIsPrivate = true;
            MockGroup firstPrivateGroup = new MockGroup(firstPrivateGroupId, firstPrivateGroupName, firstPrivateGroupIsPrivate);
            groupRepository.Insert(firstPrivateGroup);

            controller = new GroupController(premiumUserRepository, groupRepository);
            
        }
        [TestMethod]
        public void GroupController_Constructor()
        {         
            Assert.IsNotNull(controller);
        }

        [TestMethod]
        public void GetGroup_GroupThatExists()
        {
            string expectedGroupName = "Group1";
            int expectedGroupId = 1;
            bool expectedIsPrivate = false;

            MockGroup groupReturnedByController = controller.GetGroup(expectedGroupId);

            Assert.AreEqual(expectedGroupId, groupReturnedByController.GetId());
            Assert.AreEqual(expectedGroupName, groupReturnedByController.GroupName);
            Assert.AreEqual(expectedIsPrivate, groupReturnedByController.IsPrivate);          
        }
        [TestMethod]
        public void GetGroup_GroupThatDoesntExists()
        {
            MockGroup expectedResult = null;

            MockGroup groupReturnedByController = controller.GetGroup(-1);
            Assert.AreEqual(groupReturnedByController, expectedResult);

        }
        [TestMethod]
        public void MatchesFilter_GroupIsNull()
        {
            UserWrapper user = new UserWrapper(1);

            int expectedResult = 0;

            List<MockGroup> emptyList = controller.ExecuteSearch(user, null);
            Assert.AreEqual(expectedResult, emptyList.Count);
        }

        [TestMethod]
        public void ExecuteSearch_RegularUserGetsAllPublicGroupsWhenNotUsingFilter()
        {
            int expectedNrOfGroups = 2;
            UserWrapper user = new UserWrapper(2);
            string filter = string.Empty;
            Assert.AreEqual(expectedNrOfGroups, controller.ExecuteSearch(user, filter).Count);
        }
        [TestMethod]
        public void ExecuteSearch_RegularUserGetsAllPublicGroupMatchingToFilter()
        {
            int expectedNrOfGroups = 1;
            UserWrapper user = new UserWrapper(2);
            string filter = "Filter";
            Assert.AreEqual(expectedNrOfGroups, controller.ExecuteSearch(user, filter).Count);
        }
        
        [TestMethod]
        public void ExecuteSearch_PremiumUserGetsAllGroupsWhenNotUsingFilter()
        {
            int expectedNrOfGroups = 3;
            UserWrapper user = new UserWrapper(1);
            string filter = string.Empty;
            List<MockGroup> groups = controller.ExecuteSearch(user, filter);
            
            Assert.AreEqual(expectedNrOfGroups, controller.ExecuteSearch(user, filter).Count);
        }
        [TestMethod]
        public void ExecuteSearch_PremiumUserGetsAllGroupsMatchingToFilter()
        {
            int expectedNrOfGroups = 2;
            UserWrapper user = new UserWrapper(1);
            string filter = "Filter";
            Assert.AreEqual(expectedNrOfGroups, controller.ExecuteSearch(user, filter).Count);
        }

    }
}
