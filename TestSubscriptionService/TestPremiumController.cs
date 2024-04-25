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

namespace TestSubscriptionService
{
    [TestClass]
    public class TestPremiumController
    {
        private IMockPostRepository mockPostRepository;
        private IPremiumPostRepository premiumPostRepository;
        private IPremiumUserRepository premiumUserRepository;
        private IPremiumPostController premiumPostController;
        [TestInitialize]
        public void TestInitialize()
        {
            mockPostRepository = new MockPostInMemoryRepository();
            premiumPostRepository = new PremiumPostInMemoryRepository();
            premiumUserRepository = new PremiumUserInMemoryRepository();
            MockPost post1 = new MockPost(5, 1, string.Empty, string.Empty, DateTime.Now);
            MockPost post3 = new MockPost(7, 1, string.Empty, string.Empty, DateTime.Now);
            mockPostRepository.Insert(post1);
            mockPostRepository.Insert(post3);

            premiumPostController = new PremiumPostController(mockPostRepository, premiumPostRepository, premiumUserRepository);
            UserWrapper premiumUser = new UserWrapper(1, "mail", "Dorian", "Pop", DateTime.Now);
            premiumUserRepository.Insert(premiumUser);
        }
        [TestMethod]
        public void AddPremiumPost_WhenPosterIsPremium_ShouldReturnTrue()
        {
            MockPost post = new MockPost(1, 1, string.Empty, string.Empty, DateTime.Now);
            bool expectedResult = true;
            bool result = premiumPostController.AddPremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void AddPremiumPost_WhenPosterIsNotPremium_ShouldReturnFalse()
        {
            MockPost post = new MockPost(2, 2, string.Empty, string.Empty, DateTime.Now);
            bool expectedResult = false;
            bool result = premiumPostController.AddPremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumPost_WhenPosterIsPremium_ShouldReturnTrue()
        {
            MockPost post = new MockPost(3, 1, string.Empty, string.Empty, DateTime.Now);
            mockPostRepository.Insert(post);
            bool expectedResult = true;
            bool result = premiumPostController.DeletePremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumPost_WhenPosterIsNotPremium_ShouldReturnFalse()
        {
            MockPost post = new MockPost(4, 2, string.Empty, string.Empty, DateTime.Now);
            mockPostRepository.Insert(post);
            bool expectedResult = false;
            bool result = premiumPostController.DeletePremiumPost(post);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void GetPostQueue_ShouldReturnPremiumPostsBeforeRegularPosts()
        {
            MockPost post2 = new MockPost(6, 1, string.Empty, string.Empty, DateTime.Now);
            MockPost post4 = new MockPost(8, 1, string.Empty, string.Empty, DateTime.Now);
            premiumPostController.AddPremiumPost(post2);
            premiumPostController.AddPremiumPost(post4);
            PriorityQueue<MockPost, int> result = premiumPostController.GetPostQueue();
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(6, result.Dequeue().Id);
            Assert.AreEqual(8, result.Dequeue().Id);
            Assert.AreEqual(5, result.Dequeue().Id);
            Assert.AreEqual(7, result.Dequeue().Id);
        }
    }
}
