using ISSProject.Common.Logging;
using ISSProject.Common.Mock;
using ISSProject.Common;
using ISSProject.ScamBots;
using ISSProject_Regenerated.ScamBots.Service;
using Moq;

namespace ScamBotsFakeUserController
{
    [TestClass]
    public class FakeUserControllerTests
    {
        private Mock<IFakeUserRepository> mockUserRepo;
        private Mock<ISizedRepository<MockUser, int>> mockAllUsersRepo;
        private Mock<ISizedRepository<MockMessage, int>> mockMessageRepo;
        private Mock<IScamMessageGenerator> mockMessageGenerator;
        private Mock<ILoggingModule> mockLogger;
        private FakeUserController controller;

        [TestInitialize]
        public void Setup()
        {
            // Setup mocks
            mockUserRepo = new Mock<IFakeUserRepository>();
            mockAllUsersRepo = new Mock<ISizedRepository<MockUser, int>>();
            mockMessageRepo = new Mock<ISizedRepository<MockMessage, int>>();
            mockMessageGenerator = new Mock<IScamMessageGenerator>();
            mockLogger = new Mock<ILoggingModule>();

            // Initialize the controller with mocks
            controller = new FakeUserController(
                mockUserRepo.Object,
                mockAllUsersRepo.Object,
                mockMessageRepo.Object,
                mockMessageGenerator.Object,
                mockLogger.Object,
                24, 2, 5); // Parameters: cooldown, messages per bot, population percentage
        }

        [TestMethod]
        public void GenerateBotAccounts_ShouldGenerateExpectedNumberOfAccounts()
        {
            // Arrange
            int expectedNumberOfAccounts = 10;
            mockUserRepo.Setup(r => r.Size()).Returns(0);
            mockAllUsersRepo.Setup(r => r.Size()).Returns(200); // Assuming 200 legitimate users
            mockUserRepo.Setup(r => r.Insert(It.IsAny<MockUser>())).Returns(true);

            // Act
            int result = controller.GenerateBotAccounts();

            // Assert
            Assert.AreEqual(expectedNumberOfAccounts, result);
            mockUserRepo.Verify(r => r.Insert(It.IsAny<MockUser>()), Times.Exactly(expectedNumberOfAccounts));
        }

        [TestMethod]
        public void StartAttackWave_ShouldLogAndSendMessages()
        {
            // Arrange
            int expectedMessagesSent = 10;
            mockUserRepo.Setup(r => r.Size()).Returns(10);
            mockUserRepo.Setup(r => r.NumberOfBannedFakeAccounts()).Returns(0);
            mockMessageGenerator.Setup(g => g.GenerateScamMessage()).Returns("Scam Message");

            // Act
            controller.StartAttackWave();

            // Assert
            mockLogger.Verify(l => l.Log(It.IsAny<LogSeverity>(), It.IsAny<string>()), Times.AtLeastOnce);
            mockMessageRepo.Verify(r => r.Insert(It.IsAny<MockMessage>()), Times.Exactly(expectedMessagesSent));
        }
    }
}