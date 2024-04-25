namespace TestSubscriptionService
{
    using ISSProject.Common.Mikha.Controllers;
    using ISSProject.Common.Repository;
    using ISSProject.Common.Wrapper;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
    using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
    using Moq;

    [TestClass]
    public class TestPremiumMessageController
    {
        private Mock<IPremiumUserRepository> premiumUserRepository;
        private Mock<IMessageRepository> messageRepository;
        private Mock<IPremiumMessageRepository> premiumMessageRepository;
        private IPremiumMessageController premiumMessageController;

        [TestInitialize]
        public void TestInitializer()
        {
            premiumUserRepository = new Mock<IPremiumUserRepository>();
            messageRepository = new Mock<IMessageRepository>();
            premiumMessageRepository = new Mock<IPremiumMessageRepository>();
            premiumMessageController = new PremiumMessageController(premiumUserRepository.Object, messageRepository.Object, premiumMessageRepository.Object);
        }
        [TestMethod]
        public void AddPremiumMessage_WhenMessageIsPremiumAndSenderIsPremium_ShouldReturnTrue()
        {
            bool expectedResult = true;
            UserWrapper expectedUser = new UserWrapper(1, "mail", "name", "lName", new DateTime(2014, 12, 12, 0, 0, 0));
            MessageWrapper messageSent = new MessageWrapper(1, 1, 2, "Hello", new DateTime(2024, 12, 15, 0, 0, 0));
            premiumUserRepository.Setup(repo => repo.ById(1)).Returns(expectedUser);
            messageRepository.Setup(repo => repo.Insert(messageSent)).Returns(true);
            premiumMessageRepository.Setup(repo => repo.Insert(messageSent)).Returns(true);
            bool result = premiumMessageController.AddPremiumMessage(messageSent);
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
