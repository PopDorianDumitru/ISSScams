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
        public void GetPureReference_ValidGroup_ReferenceShouldBeEqual()
        {
            string expectedGroupName = "Soccer";
            int expectedId = 1;
            bool expectedIsPrivate = false;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            GroupWrapper groupWrapper = new GroupWrapper(mockGroup);
            MockGroup result = groupWrapper.GetPureReference();

            Assert.IsTrue(mockGroup.Equals(result));
        }

        [TestMethod]
        public void Clone_CreateValidWrapper_ClonedWrapperShouldHaveTheSameGroup()
        {
            string expectedGroupName = "Soccer";
            int expectedId = 1;
            bool expectedIsPrivate = false;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            GroupWrapper groupWrapper = new GroupWrapper(mockGroup);
            GroupWrapper clonedGroupWrapperToTest = (GroupWrapper)groupWrapper.Clone();

            Assert.IsTrue(clonedGroupWrapperToTest.Equals(mockGroup));
        }

        [TestMethod]
        public void MockGroupAddMember_AddValidIdTomptyGroup_CountShouldBeOne()
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
    }
}
