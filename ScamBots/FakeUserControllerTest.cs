using Moq;
using ISSProject_Regenerated.ScamBots;
namespace ScamBots.Tests
{
    [TestClass]
    public class FakeUserControllerTest
    {
        private Mock<FakeU> mockUserRepo;
        private Mock<ISizedRepository<MockUser, int>> mockAllUsersRepo;
        private Mock<ISizedRepository<MockMessage, int>> mockMessageRepo;
        private Mock<IScamMessageGenerator> mockScamMessageGen;
        private Mock<IFakeUserGenerator> mockUserGen;
        private FakeUserController controller;

        [TestInitialize]
        public void Setup()
        {
            mockUserRepo = new Mock<IFakeUserRepository>();
            mockAllUsersRepo = new Mock<ISizedRepository<MockUser, int>>();
            mockMessageRepo = new Mock<ISizedRepository<MockMessage, int>>();
            mockScamMessageGen = new Mock<IScamMessageGenerator>();
            mockUserGen = new Mock<IFakeUserGenerator>();
            controller = new FakeUserController(24, 2, 5);
        }
    }
}