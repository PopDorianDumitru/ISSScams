namespace TestSubscriptionService
{
    using System;
    using ISSProject.Common.Mikha;

    [TestClass]
    public class TestMockPost
    {
        [TestMethod]
        public void Clone_CloningAnInstance_ShouldReturnAnInstanceIdenticalToOriginal()
        {
            int id = 1;
            int posterId = 1;
            string postContent = "content";
            string postTitle = "title";
            DateTime postDate = new DateTime(2024, 12, 15, 0, 0, 0);

            MockPost mockPost = new MockPost(id, posterId, postTitle, postContent, postDate);
            MockPost mockPostClone = (MockPost)mockPost.Clone();

            bool expectedResult = true;

            Assert.AreEqual(expectedResult, mockPost.Equals(mockPostClone));
        }
    }
}
