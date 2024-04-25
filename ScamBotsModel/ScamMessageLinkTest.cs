using ISSProject.ScamBots.Model;

namespace ScamBotsModel
{
    [TestClass]
    public class ScamMessageLinkTests
    {
        [TestMethod]
        public void Constructor_WithIdAndLinkUrl_ShouldSetPropertiesCorrectly()
        {
            int expectedId = 1;
            string expectedLinkUrl = "http://example.com";

            ScamMessageLink link = new ScamMessageLink(expectedId, expectedLinkUrl);

            Assert.AreEqual(expectedId, link.Id);
            Assert.AreEqual(expectedLinkUrl, link.LinkUrl);
        }

        [TestMethod]
        public void Constructor_WithOnlyLinkUrl_ShouldSetDefaultId()
        {
            string expectedLinkUrl = "http://example.com";

            ScamMessageLink link = new ScamMessageLink(expectedLinkUrl);

            Assert.AreEqual(-1, link.Id);
            Assert.AreEqual(expectedLinkUrl, link.LinkUrl);
        }

        [TestMethod]
        public void GetId_ShouldReturnCorrectId()
        {
            ScamMessageLink link = new ScamMessageLink(10, "http://example.com");

            int id = link.GetId();

            Assert.AreEqual(10, id);
        }

        [TestMethod]
        public void Clone_ShouldCreateADeepCopy()
        {
            ScamMessageLink originalLink = new ScamMessageLink(10, "http://example.com");

            ScamMessageLink clonedLink = (ScamMessageLink)originalLink.Clone();

            Assert.AreEqual(originalLink.Id, clonedLink.Id);
            Assert.AreEqual(originalLink.LinkUrl, clonedLink.LinkUrl);
            Assert.AreNotSame(originalLink, clonedLink);
        }
    }
}