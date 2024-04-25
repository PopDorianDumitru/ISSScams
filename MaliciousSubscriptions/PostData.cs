using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace MaliciousSubscriptionsDomain
{
    [TestClass]
    public class PostDataTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeSelfJSONToEmptyString()
        {
            var postData = new PostData();
            Assert.AreEqual(string.Empty, postData.SelfJSON);
        }
    }
}
