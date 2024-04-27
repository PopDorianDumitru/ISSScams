using ISSProject.ScamBots.Service;

namespace ScamBotsService
{
    [TestClass]
    public class FakeUserGeneratorTests
    {
        [TestMethod]
        public void GenerateFakeUser_UserFirstNameIsNullOrEmpty_ShouldReturnFalse()
        {
            var fakeUserGenerator = new FakeUserGenerator();
            var fakeUser = fakeUserGenerator.GenerateFakeUser();
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.FirstName));
        }

        [TestMethod]
        public void GenerateFakeUser_UserLastNameIsNullOrEmpty_ShouldReturnFalse()
        {
            var fakeUserGenerator = new FakeUserGenerator();
            var fakeUser = fakeUserGenerator.GenerateFakeUser();
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.LastName));
        }

        [TestMethod]
        public void GenerateFakeUser_UserEmailIsNullOrEmpty_ShouldReturnFalse()
        {
            var fakeUserGenerator = new FakeUserGenerator();
            var fakeUser = fakeUserGenerator.GenerateFakeUser();
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.Email));
        }

        [TestMethod]
        public void GenerateFakeUser_UserPasswordIsNullOrEmpty_ShouldReturnFalse()
        {
            var fakeUserGenerator = new FakeUserGenerator();
            var fakeUser = fakeUserGenerator.GenerateFakeUser();
            Assert.IsFalse(string.IsNullOrWhiteSpace(fakeUser.Password));
        }

        [TestMethod]
        public void GenerateFakeUsers_FakeUsersCountIsEqual_ShouldReturnTrue()
        {
            var fakeUserGenerator = new FakeUserGenerator();
            int count = 5;
            var fakeUsers = fakeUserGenerator.GenerateFakeUsers(count);
            Assert.IsTrue(count.Equals(fakeUsers.Count));
        }
    }
}