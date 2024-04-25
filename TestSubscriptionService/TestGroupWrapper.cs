using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha.Groups;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestGroupWrapper
    {
        [TestMethod]
        public void GroupWrapper_GetPureReference()
        {
            string expectedGroupName = "Soccer";
            int expectedId = 1;
            bool expectedIsPrivate = false;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            GroupWrapper groupWrapper = new GroupWrapper(mockGroup);
            MockGroup result = groupWrapper.GetPureReference();
            Assert.AreEqual(mockGroup.GroupName, expectedGroupName);
            Assert.AreEqual(mockGroup.Id, expectedId);
            Assert.AreEqual(mockGroup.IsPrivate, expectedIsPrivate);
        }

        [TestMethod]
        public void GroupWrapper_CloneGroupWrapper()
        {
            string expectedGroupName = "Soccer";
            int expectedId = 1;
            bool expectedIsPrivate = false;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            GroupWrapper groupWrapper = new GroupWrapper(mockGroup);
            GroupWrapper clonedGroupWrapperToTest = (GroupWrapper)groupWrapper.Clone();
            Assert.AreEqual(clonedGroupWrapperToTest.GetGroupName(), expectedGroupName);
            Assert.AreEqual(expectedIsPrivate, clonedGroupWrapperToTest.GetGroupVisibility());
            Assert.AreEqual(expectedId, clonedGroupWrapperToTest.GetId());
        }

        [TestMethod]
        public void GroupWrapper_AddingAUserShouldIncreaseTheNumberOfUsers()
        {
            string expectedGroupName = "Soccer";
            int expectedId = 1;
            bool expectedIsPrivate = false;
            int expectedNrOfUsersAfterAddingOneUser = 1;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            GroupWrapper groupWrapper = new GroupWrapper(mockGroup);
            mockGroup.MembersID.Add(1);
            int finalNumberOfUsers = groupWrapper.GetUsersIDs().Count;
            Assert.AreEqual(expectedNrOfUsersAfterAddingOneUser, finalNumberOfUsers);
        }
        [TestMethod]
        public void GroupWrapper_FetchUsingId()
        {
            string expectedGroupName = "Soccer";
            int expectedId = 1;
            bool expectedIsPrivate = false;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            GroupWrapper groupWrapper = new GroupWrapper(mockGroup);
            MockGroup result = groupWrapper.FetchUsingId(1);
            Assert.AreEqual(expectedGroupName, result.GroupName);
            Assert.AreEqual(expectedId, result.Id);
            Assert.AreEqual(expectedIsPrivate, result.IsPrivate);
        }
    }
}
