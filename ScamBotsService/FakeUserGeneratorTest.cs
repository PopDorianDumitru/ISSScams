using ISSProject.ScamBots.Service;

namespace ScamBotsService
{
    [TestClass]
    public class FakeUserGeneratorTests
    {
        [TestMethod]
        public void TestGenerateFakeUser()
        { 
            var fakeUserGenerator = new FakeUserGenerator();

            var fakeUser = fakeUserGenerator.GenerateFakeUser();

            Assert.IsNotNull(fakeUser);
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.FirstName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.LastName));
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.Email));
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.Password));
        }

        [TestMethod]
        public void TestGenerateFakeUsers()
        {
            var fakeUserGenerator = new FakeUserGenerator();
            int count = 5;

            var fakeUsers = fakeUserGenerator.GenerateFakeUsers(count);

            Assert.IsNotNull(fakeUsers);
            Assert.AreEqual(count, fakeUsers.Count);
            foreach (var user in fakeUsers)
            {
                Assert.IsNotNull(user);
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.FirstName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.LastName));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.Email));
                Assert.IsFalse(string.IsNullOrWhiteSpace(user.Password));
            }
        }
    }
}