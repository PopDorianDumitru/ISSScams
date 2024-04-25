using ISSProject.Common.Mikha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestPostWrapper
    {
        [TestMethod]
        public void PostWrapper_GetPureReference()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);
            string expectedDateString = "1 - 1 - content - title - 15/12/2024 00:00:00";

            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);
            PostWrapper postWrapper = new PostWrapper(mockPost);
            Assert.AreEqual(mockPost, postWrapper.GetPureReference());
        }

        [TestMethod]
        public void PostWrapper_FetchUsingId()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);
            string expectedDateString = "1 - 1 - content - title - 15/12/2024 00:00:00";
            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);
            PostWrapper postWrapper = new PostWrapper(mockPost);
            Assert.AreEqual(mockPost, postWrapper.FetchUsingId(1));
        }

        [TestMethod]
        public void PostWrapper_GetId()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);
            string expectedDateString = "1 - 1 - content - title - 15/12/2024 00:00:00";
            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);
            PostWrapper postWrapper = new PostWrapper(mockPost);

            Assert.AreEqual(expectedId, postWrapper.GetId());
        }
        [TestMethod]
        public void PostWrapper_Clone()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);
            string expectedDateString = "1 - 1 - content - title - 15/12/2024 00:00:00";
            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);

            PostWrapper postWrapper = new PostWrapper(mockPost);
            PostWrapper expectedResult = (PostWrapper)postWrapper.Clone();
            Assert.AreEqual(expectedResult.GetId(), expectedId);
            Assert.AreEqual(expectedResult.GetUserId(), expectedPosterId);
            Assert.AreEqual(expectedResult.GetPureReference(), postWrapper.GetPureReference());
        }
        [TestMethod]
        public void PostWrapper_GetUserId()
        {
            int expectedId = 1;
            int expectedPosterId = 1;
            string expectedPostContent = "content";
            string expectedPostTitle = "title";
            DateTime expectedPostDate = new DateTime(2024, 12, 15, 0, 0, 0);
            string expectedDateString = "1 - 1 - content - title - 15/12/2024 00:00:00";
            MockPost mockPost = new MockPost(expectedId, expectedPosterId, expectedPostTitle, expectedPostContent, expectedPostDate);

            PostWrapper postWrapper = new PostWrapper(mockPost);
            Assert.AreEqual(expectedPosterId, postWrapper.GetUserId());
        }

    }
}
