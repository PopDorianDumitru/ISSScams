namespace TestSubscriptionService
{
    using ISSProject.Common.Mikha.Controllers;
    using ISSProject.Common.Repository;
    using ISSProject.Common.Wrapper;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;

    [TestClass]
    public class TestPremiumMessageController
    {
        private IPremiumUserRepository premiumUserRepository;
        private IMessageRepository messageRepository;
        private IPremiumMessageRepository premiumMessageRepository;
        private IPremiumMessageController premiumMessageController;

        [TestInitialize]
        public void TestInitializer()
        {
            premiumUserRepository = new PremiumUserInMemoryRepository();
            messageRepository = new MessageRepository();
            premiumMessageRepository = new PremiumMessageInMemoryRepository();
            UserWrapper premiumUser = new UserWrapper(1);
            premiumUserRepository.Insert(premiumUser);
            premiumMessageController = new PremiumMessageController(premiumUserRepository, messageRepository, premiumMessageRepository);
            MessageWrapper message1 = new MessageWrapper(1, 1, 2, "Hello", new DateTime(2024, 12, 15, 0, 0, 0));
            MessageWrapper message2 = new MessageWrapper(2, 2, 1, "Hi", new DateTime(2024, 12, 15, 0, 0, 0));
            premiumMessageRepository.Insert(message1);
            messageRepository.Insert(message1);
            premiumUserRepository = new PremiumUserInMemoryRepository();
        }
        [TestMethod]
        public void AddPremiumMessage_WhenMessageIsPremiumAndSenderIsPremium_ShouldReturnTrue()
        {
            MessageWrapper message = new MessageWrapper(3, 1, 2, "Hello", new DateTime(2024, 12, 15, 0, 0, 0));
            bool expectedResult = true;
            bool result = premiumMessageController.AddPremiumMessage(message);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void AddPremiumMessage_WhenMessageIsNotPremium_ShouldReturnFalse()
        {
            MessageWrapper message = new MessageWrapper(4, 3, 2, "Hello", new DateTime(2024, 12, 15, 0, 0, 0));
            bool expectedResult = false;
            bool result = premiumMessageController.AddPremiumMessage(message);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumMessage_WhenMessageIsPremium_ShouldReturnTrue()
        {
            MessageWrapper message = new MessageWrapper(5, 1, 2, "Hello", new DateTime(2024, 12, 15, 0, 0, 0));
            bool expectedResult = true;
            bool result = premiumMessageController.DeletePremiumMessage(message);
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void DeletePremiumMessage_WhenMessageIsNotPremium_ShouldReturnFalse()
        {
            MessageWrapper message = new MessageWrapper(6, 3, 2, "Hello", new DateTime(2024, 12, 15, 0, 0, 0));
            bool expectedResult = false;
            bool result = premiumMessageController.DeletePremiumMessage(message);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
