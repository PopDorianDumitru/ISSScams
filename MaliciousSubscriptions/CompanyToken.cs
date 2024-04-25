using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace MaliciousSubscriptions
{
    [TestClass]
    public class CompanyTokenTests
    {
        [TestMethod]
        public void TestConstructor_ShouldInitializePropertiesCorrectly()
        {
            int expectedId = 1;
            string expectedCompanyName = "Test Company";
            string expectedLinkToAPI = "https://api.test.com";
            string expectedToken = "testtoken123";
            int expectedServiceSeverity = 0;

            var companyToken = new CompanyToken(expectedId, expectedCompanyName, expectedLinkToAPI, expectedToken, expectedServiceSeverity);

            Assert.AreEqual(expectedId, companyToken.GetId());
            Assert.AreEqual(expectedCompanyName, companyToken.GetCompanyName());
            Assert.AreEqual(expectedLinkToAPI, companyToken.GetLinkToAPI());
            Assert.AreEqual(expectedToken, companyToken.GetToken());
            Assert.AreEqual(expectedServiceSeverity, companyToken.GetServiceSeverity());
        }
    }
}