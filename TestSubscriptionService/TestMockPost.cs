namespace TestSubscriptionService
{
    using System;
    using ISSProject.Common.Mikha;

    [TestClass]
    public class TestMockPost
    {
        [TestMethod]
        public void MockPost_Clone()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);

            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);
            MockPost mockPostClone = (MockPost)mockPost.Clone();

            Assert.AreEqual(expectedPostContent, mockPostClone.PostContent);
            Assert.AreEqual(expectedPosterId, mockPostClone.PosterId);
            Assert.AreEqual(expectedPostTitle, mockPostClone.PostTitle);
            Assert.AreEqual(expectedPostDate, mockPostClone.PostDate);
            Assert.AreEqual(expectedId, mockPostClone.Id);
        }

        [TestMethod]
        public void MockPost_returnsCorrectId()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);

            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);
            Assert.AreEqual(expectedId, mockPost.GetId());
        }

        [TestMethod]
        public void MockPost_generatesCorrectString()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);
            string expectedDateString = "1 - 1 - title - content - 15/12/2024 00:00:00";
            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);

            Assert.AreEqual(expectedDateString, mockPost.ToString());
        }
    }
}
