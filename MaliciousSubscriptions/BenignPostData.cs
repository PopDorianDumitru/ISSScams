using ISSProject.MaliciousSubscriptionsBackend.Domain;
using ISSProject.MaliciousSubscriptionsBackend.Service;

namespace MaliciousSubscriptions
{
    [TestClass]
    public class BenignPostDataTests
    {
        [TestMethod]
        public void TestConstructor_ShouldSetEmailCorrectly()
        {
            string expectedEmail = "test@example.com";
            var postData = new BenignPostData(expectedEmail);
            Assert.AreEqual(expectedEmail, postData.Email);
        }
    }
}