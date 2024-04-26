namespace TestSubscriptionService
{
    using System;
    using ISSProject.Common.Mikha;

    [TestClass]
    public class TestPostWrapper
    {
        [TestMethod]
        public void Clone_CloningAnInstanceOfObject_ShouldReturnAnIdenticalInstance()
        {
            int id = 1;
            int posterId = 1;
            string postContent = "content";
            string postTitle = "title";
            DateTime postDate = new DateTime(2024, 12, 15, 0, 0, 0);
            MockPost mockPost = new MockPost(id, posterId, postTitle, postContent, postDate);

            PostWrapper postWrapper = new PostWrapper(mockPost);
            PostWrapper postWrapperClone = (PostWrapper)postWrapper.Clone();

            bool expectedResult = true;
            Assert.AreEqual(expectedResult, postWrapperClone.Equals(postWrapper));
        }
    }
}
