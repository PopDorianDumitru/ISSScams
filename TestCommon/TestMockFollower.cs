using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace TestCommon
{
    [TestClass]
    public class TestMockFollower
    {
        private MockFollower mockFollower;

        [TestInitialize]
        public void TestInitialize()
        {
            mockFollower = new MockFollower(1, 2, 3);
        }

        [TestMethod]
        public void Clone_ValidMockInstance_ReturnsACloneOfTheObject()
        {
            MockFollower clone = (MockFollower)mockFollower.Clone();
            Assert.AreEqual(mockFollower.Id, clone.Id);
            Assert.AreEqual(mockFollower.UserId, clone.UserId);
            Assert.AreEqual(mockFollower.FollowedUserId, clone.FollowedUserId);
        }

        [TestMethod]
        public void GetId_()
        {
            Assert.AreEqual(1, mockFollower.GetId());
        }
    }
}
