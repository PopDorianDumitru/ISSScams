
using ISSProject.ScamBots.Model;

namespace ScamBotsModel
{
    [TestClass]
    public class ScamMessageTemplateTests
    {
        [TestMethod]
        public void Constructor_WithIdAndMessageContent_ShouldSetPropertiesCorrectly()
        {
            int expectedId = 1;
            string expectedMessageContent = "You have won a prize!";

            ScamMessageTemplate template = new ScamMessageTemplate(expectedId, expectedMessageContent);

            Assert.AreEqual(expectedId, template.Id);
            Assert.AreEqual(expectedMessageContent, template.MessageContent);
        }

        [TestMethod]
        public void Constructor_WithOnlyMessageContent_ShouldSetDefaultId()
        {
            string expectedMessageContent = "Congratulations! You are our lucky winner!";

            ScamMessageTemplate template = new ScamMessageTemplate(expectedMessageContent);

            Assert.AreEqual(-1, template.Id);  
            Assert.AreEqual(expectedMessageContent, template.MessageContent);
        }

        [TestMethod]
        public void GetId_ShouldReturnCorrectId()
        {
            ScamMessageTemplate template = new ScamMessageTemplate(10, "Check your prize now!");

            int id = template.GetId();

            Assert.AreEqual(10, id);
        }

        [TestMethod]
        public void Clone_ShouldCreateADeepCopy()
        {
            ScamMessageTemplate originalTemplate = new ScamMessageTemplate(10, "Exclusive offer for you!");

            ScamMessageTemplate clonedTemplate = (ScamMessageTemplate)originalTemplate.Clone();

            Assert.AreEqual(originalTemplate.Id, clonedTemplate.Id);
            Assert.AreEqual(originalTemplate.MessageContent, clonedTemplate.MessageContent);
            Assert.AreNotSame(originalTemplate, clonedTemplate);  
        }
    }
}
