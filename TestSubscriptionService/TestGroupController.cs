using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ISSProject_Regenerated.SubscriptionServiceBackend.Groups;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
using ISSProject.Common.Mikha.Groups;
using ISSProject.Common.Wrapper;
using Moq;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestGroupController
    {
        private Mock<IPremiumUserRepository> premiumUserRepository;
        private Mock<IMockGroupRepository> groupRepository;
        private IGroupController groupController;
        [TestInitialize]
        public void TestInitializer()
        {
            premiumUserRepository = new Mock<IPremiumUserRepository>();
            groupRepository = new Mock<IMockGroupRepository>();
            groupController = new GroupController(premiumUserRepository.Object, groupRepository.Object);
        }

        [TestMethod]
        public void GetGroup_GroupThatExists_ShouldReturnTheRequestedGroup()
        {
            string expectedGroupName = "Group1";
            int expectedGroupId = 1;
            bool expectedIsPrivate = false;

            MockGroup createdGroup = new MockGroup(expectedGroupId, expectedGroupName, expectedIsPrivate);
            groupRepository.Setup(repo => repo.ById(expectedGroupId)).Returns(createdGroup);

            MockGroup groupReturnedByController = groupController.GetGroup(expectedGroupId);

            Assert.IsTrue(groupReturnedByController.Equals(createdGroup));

        }
        [TestMethod]
        public void GetGroup_GroupThatDoesntExist_ShouldReturnNull()
        {
            MockGroup expectedResult = null;

            groupRepository.Setup(repo => repo.ById(-1)).Returns((MockGroup)null);
            MockGroup groupReturnedByController = groupController.GetGroup(-1);
            Assert.AreEqual(groupReturnedByController, expectedResult);
        }

        // [TestMethod]
        // public void MatchesFilter_GroupIsNull_ShouldReturnEmptyList()
        // {
        //    UserWrapper user = new UserWrapper(1);
        //    premiumUserRepository.Setup(repo => repo.ById(1)).Returns(user);

        // int expectedResult = 0;

        // List<MockGroup> emptyList = groupController.ExecuteSearch(user, null);
        //    Assert.AreEqual(expectedResult, emptyList.Count);
        // }
        [TestMethod]
        public void ExecuteSearch_RegularUserGetsAllPublicGroupsWhenNotUsingFilter_ShouldReturnLengthTwo()
        {
            int expectedNrOfGroups = 2;
            UserWrapper user = new UserWrapper(2);
            premiumUserRepository.Setup(repo => repo.ById(2)).Returns((UserWrapper)null);
            List<MockGroup> mockGroups = new List<MockGroup>();
            mockGroups.Add(new MockGroup(1, "Bani", false));
            mockGroups.Add(new MockGroup(2, "Fotbal", false));
            IEnumerable<MockGroup> mockGroupsEnumerable = mockGroups;
            groupRepository.Setup(repo => repo.All()).Returns(mockGroupsEnumerable);

            string filter = string.Empty;
            Assert.AreEqual(expectedNrOfGroups, groupController.ExecuteSearch(user, filter).Count);
        }
        [TestMethod]
        public void ExecuteSearch_RegularUserGetsAllPublicGroupMatchingToFilter_ShouldReturnLengthOne()
        {
            int expectedNrOfGroups = 1;
            UserWrapper user = new UserWrapper(2);
            premiumUserRepository.Setup(repo => repo.ById(2)).Returns((UserWrapper)null);
            List<MockGroup> mockGroups = new List<MockGroup>();
            mockGroups.Add(new MockGroup(1, "Filter", false));
            mockGroups.Add(new MockGroup(2, "Fotbal", false));
            IEnumerable<MockGroup> mockGroupsEnumerable = mockGroups;
            groupRepository.Setup(repo => repo.All()).Returns(mockGroupsEnumerable);
            string filter = "Filter";
            Assert.AreEqual(expectedNrOfGroups, groupController.ExecuteSearch(user, filter).Count);
        }
        [TestMethod]
        public void ExecuteSearch_PremiumUserGetsAllGroupsWhenNotUsingFilter_ShouldReturnLengthThree()
        {
            int expectedNrOfGroups = 3;
            UserWrapper user = new UserWrapper(1);
            premiumUserRepository.Setup(repo => repo.ById(1)).Returns(user);
            List<MockGroup> mockGroups = new List<MockGroup>();
            mockGroups.Add(new MockGroup(1, "Filter", false));
            mockGroups.Add(new MockGroup(2, "Fotbal", false));
            mockGroups.Add(new MockGroup(3, "Basket", false));
            IEnumerable<MockGroup> mockGroupsEnumerable = mockGroups;
            groupRepository.Setup(repo => repo.All()).Returns(mockGroupsEnumerable);
            string filter = string.Empty;

            Assert.AreEqual(expectedNrOfGroups, groupController.ExecuteSearch(user, filter).Count);
        }

        [TestMethod]
        public void ExecuteSearch_PremiumUserGetsAllGroupsMatchingToFilter_ShouldReturnLengthTwo()
        {
            int expectedNrOfGroups = 2;
            UserWrapper user = new UserWrapper(1);
            premiumUserRepository.Setup(repo => repo.ById(1)).Returns(user);
            List<MockGroup> mockGroups = new List<MockGroup>();
            mockGroups.Add(new MockGroup(1, "Filter", false));
            mockGroups.Add(new MockGroup(2, "Filter", false));
            mockGroups.Add(new MockGroup(3, "Basket", false));
            IEnumerable<MockGroup> mockGroupsEnumerable = mockGroups;
            groupRepository.Setup(repo => repo.All()).Returns(mockGroupsEnumerable);
            string filter = "Filter";
            Assert.AreEqual(expectedNrOfGroups, groupController.ExecuteSearch(user, filter).Count);
        }
    }
}
