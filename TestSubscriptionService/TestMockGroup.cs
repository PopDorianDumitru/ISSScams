using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha.Groups;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestMockGroup
    {

        [TestMethod]
        public void MockGroup_TestConstructor()
        {
            string expectedGroupName = "TestGroup";
            int expectedId = 1;
            bool expectedIsPrivate = true;
            int expectedNrOfMembers = 0;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            Assert.AreEqual(expectedId, mockGroup.Id);
            Assert.AreEqual(expectedGroupName, mockGroup.GroupName);
            Assert.AreEqual(expectedIsPrivate, mockGroup.IsPrivate);
            Assert.AreEqual(expectedId, mockGroup.GetId());
            Assert.AreEqual(expectedNrOfMembers, mockGroup.MembersID.Count);
        }
        [TestMethod]
        public void MockGroup_TestAddMember()
        {
            string expectedGroupName = "TestGroup";
            int expectedId = 1;
            bool expectedIsPrivate = true;
            int expectedNrOfMembersBeforeAdd = 0;
            int expectedNrOfMembersAfterAddingUser = 1;
            int expectedNrOfMembersAfterAddingSameUser = 1;
            int expectedNrOfMembersAfterAddingSecondUser = 2;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            Assert.AreEqual(expectedNrOfMembersBeforeAdd, mockGroup.MembersID.Count);
            mockGroup.AddMember(1);
            Assert.AreEqual(expectedNrOfMembersAfterAddingUser, mockGroup.MembersID.Count);
            mockGroup.AddMember(1);
            Assert.AreEqual(expectedNrOfMembersAfterAddingSameUser, mockGroup.MembersID.Count);
            mockGroup.AddMember(2);
            Assert.AreEqual(expectedNrOfMembersAfterAddingSecondUser, mockGroup.MembersID.Count);
        }

        [TestMethod]
        public void MockGroup_TestClone()
        {
            string expectedGroupName = "TestGroup";
            int expectedId = 1;
            bool expectedIsPrivate = true;
            int expectedNrOfMembers = 0;
            MockGroup mockGroup = new MockGroup(1, "TestGroup", true);
            MockGroup clone = (MockGroup)mockGroup.Clone();
            Assert.AreEqual(expectedId, clone.Id);
            Assert.AreEqual(expectedGroupName, clone.GroupName);
            Assert.AreEqual(expectedIsPrivate, clone.IsPrivate);
            Assert.AreEqual(expectedNrOfMembers, clone.MembersID.Count);
        }
    }
}
