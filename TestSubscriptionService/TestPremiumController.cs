using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha;
using ISSProject.Common.Mikha.Controllers;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.SubscriptionServiceBackend.Post;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
using Moq;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestPremiumController
    {
        private Mock<IMockPostRepository> mockPostRepository;
        private Mock<IPremiumPostRepository> premiumPostRepository;
        private Mock<IPremiumUserRepository> premiumUserRepository;
        private IPremiumPostController premiumPostController;
        [TestInitialize]
        public void TestInitialize()
        {
            mockPostRepository = new Mock<IMockPostRepository>();
            premiumPostRepository = new Mock<IPremiumPostRepository>();
            premiumUserRepository = new Mock<IPremiumUserRepository>();
            premiumPostController = new PremiumPostController(mockPostRepository.Object, premiumPostRepository.Object, premiumUserRepository.Object);
        }
        [TestMethod]
        public void AddPremiumPost_WhenPosterIsPremium_ShouldReturnTrue()
        {
            MockPost post = new MockPost(1, 1, string.Empty, string.Empty, DateTime.Now);
            bool expectedResult = true;
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(new UserWrapper(1, "email", "firstName", "lastName", DateTime.Now));
            mockPostRepository.Setup(repo => repo.Insert(post)).Returns(true);
            premiumPostRepository.Setup(repo => repo.Insert(It.IsAny<PostWrapper>())).Returns(true);
            bool result = premiumPostController.AddPremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void AddPremiumPost_WhenPosterIsPremium_ShouldInvokeInsertPost()
        {
            MockPost post = new MockPost(1, 1, string.Empty, string.Empty, DateTime.Now);
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(new UserWrapper(1, "email", "firstName", "lastName", DateTime.Now));
            mockPostRepository.Setup(repo => repo.Insert(post)).Returns(true);
            premiumPostRepository.Setup(repo => repo.Insert(It.IsAny<PostWrapper>())).Returns(true);
            premiumPostController.AddPremiumPost(post);
            mockPostRepository.Verify(repo => repo.Insert(post), Times.Once);
        }
        [TestMethod]
        public void AddPremiumPost_WhenPosterIsNotPremium_ShouldReturnFalse()
        {
            MockPost post = new MockPost(2, 2, string.Empty, string.Empty, DateTime.Now);
            bool expectedResult = false;
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(() => null);

            bool result = premiumPostController.AddPremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumPost_WhenPosterIsPremium_ShouldReturnTrue()
        {
            MockPost post = new MockPost(3, 1, string.Empty, string.Empty, DateTime.Now);
            bool expectedResult = true;
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(new UserWrapper(1, "email", "firstName", "lastName", DateTime.Now));
            premiumPostRepository.Setup(repo => repo.Delete(It.IsAny<PostWrapper>())).Returns(true);
            mockPostRepository.Setup(repo => repo.Delete(It.IsAny<MockPost>())).Returns(true);
            bool result = premiumPostController.DeletePremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumPost_WhenPosterIsNotPremium_ShouldReturnFalse()
        {
            MockPost post = new MockPost(4, 2, string.Empty, string.Empty, DateTime.Now);
            bool expectedResult = false;
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(() => null);
            bool result = premiumPostController.DeletePremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumPost_WhenPosterIsPremium_ShouldInvokeDeletePost()
        {
            MockPost post = new MockPost(5, 1, string.Empty, string.Empty, DateTime.Now);
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(new UserWrapper(1, "email", "firstName", "lastName", DateTime.Now));
            premiumPostRepository.Setup(repo => repo.Delete(It.IsAny<PostWrapper>())).Returns(true);
            mockPostRepository.Setup(repo => repo.Delete(It.IsAny<MockPost>())).Returns(true);
            premiumPostController.DeletePremiumPost(post);
            mockPostRepository.Verify(repo => repo.Delete(It.IsAny<MockPost>()), Times.Once);
        }
        [TestMethod]
        public void DeletePremiumPost_WhenPosterIsPremium_ShouldInvokePremiumDeletePost()
        {
            MockPost post = new MockPost(5, 1, string.Empty, string.Empty, DateTime.Now);
            premiumUserRepository.Setup(repo => repo.ById(post.PosterId)).Returns(new UserWrapper(1, "email", "firstName", "lastName", DateTime.Now));
            premiumPostRepository.Setup(repo => repo.Delete(It.IsAny<PostWrapper>())).Returns(true);
            mockPostRepository.Setup(repo => repo.Delete(It.IsAny<MockPost>())).Returns(true);
            premiumPostController.DeletePremiumPost(post);
            premiumPostRepository.Verify(repo => repo.Delete(It.IsAny<PostWrapper>()), Times.Once);
        }
        [TestMethod]
        public void GetPostQueue_WhenReturningThePostQueue_ShouldReturnPremiumPostsBeforeRegularPosts()
        {
            MockPost post1 = new MockPost(1, 1, string.Empty, string.Empty, DateTime.Now);
            MockPost post2 = new MockPost(2, 1, string.Empty, string.Empty, DateTime.Now);
            MockPost post3 = new MockPost(3, 1, string.Empty, string.Empty, DateTime.Now);
            List<MockPost> mockPosts = new List<MockPost>
            {
                post1,
                post2,
                post3
            };
            mockPostRepository.Setup(repo => repo.All()).Returns((IEnumerable<MockPost>)mockPosts);
            premiumPostRepository.Setup(repo => repo.ById(1)).Returns(() => null);
            premiumPostRepository.Setup(repo => repo.ById(2)).Returns(new PostWrapper(post2));
            premiumPostRepository.Setup(repo => repo.ById(3)).Returns(() => null);

            PriorityQueue<MockPost, int> result = premiumPostController.GetPostQueue();
            MockPost firstReturnedPost = result.Dequeue();
            Assert.IsTrue(firstReturnedPost.Equals(post2));
        }
    }
}
