namespace TestSubscriptionService
{
    using ISSProject.Common.Mikha.Groups;

    [TestClass]
    public class TestMockGroup
    {
        // [TestMethod]
        // public void MockGroup_TestConstructor()
        // {
        //    string expectedGroupName = "TestGroup";
        //    int expectedId = 1;
        //    bool expectedIsPrivate = true;
        //    int expectedNrOfMembers = 0;
        //    MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
        //    Assert.AreEqual(expectedId, mockGroup.Id);
        //    Assert.AreEqual(expectedGroupName, mockGroup.GroupName);
        //    Assert.AreEqual(expectedIsPrivate, mockGroup.IsPrivate);
        //    Assert.AreEqual(expectedId, mockGroup.GetId());
        //    Assert.AreEqual(expectedNrOfMembers, mockGroup.MembersID.Count);
        // }
        [TestMethod]
        public void AddMember_AddingANewMember_ShouldReturnTrue()
        {
            bool expectedResult = true;
            string groupName = "TestGroup";
            int id = 1;
            bool isPrivate = true;
            MockGroup mockGroup = new MockGroup(id, groupName, isPrivate);
            Assert.AreEqual(expectedResult, mockGroup.AddMember(1));
        }

        [TestMethod]
        public void AddMember_AddingAnExistingMember_ShouldReturnFalse()
        {
            string expectedGroupName = "TestGroup";
            int expectedId = 1;
            bool expectedIsPrivate = true;
            bool expectedResult = false;
            MockGroup mockGroup = new MockGroup(expectedId, expectedGroupName, expectedIsPrivate);
            mockGroup.AddMember(1);
            Assert.AreEqual(expectedResult, mockGroup.AddMember(1));
        }

        [TestMethod]
        public void Clone_CloningAnInstance_ShouldReturnAnInstanceIdenticalToTheOriginal()
        {
            MockGroup expectedGroup = new MockGroup(1, "TestGroup", true);
            MockGroup clone = (MockGroup)expectedGroup.Clone();
            Assert.AreEqual(expectedGroup, clone);
        }
    }
}
