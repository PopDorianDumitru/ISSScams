using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace TestCommon
{
    [TestClass]
    public class TestMockMessage
    {
        private MockMessage message;

        [TestMethod]
        public void Clone_WhenInstatiatingWithFiveParameterConstructor_ShouldReturnTheCloneOfTheObject()
        {
            message = new MockMessage(1, 2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            MockMessage clone = (MockMessage)message.Clone();

            // Assert.AreEqual(message.Id, clone.Id);
            // Assert.AreEqual(message.SenderId, clone.SenderId);
            // Assert.AreEqual(message.ReceiverId, clone.ReceiverId);
            // Assert.AreEqual(message.MessageContent, clone.MessageContent);
            // Assert.AreEqual(message.CommunicationDate, clone.CommunicationDate);
            Assert.AreEqual(message, clone);
        }

        [TestMethod]
        public void Clone_WhenInstantiatingWithFourParameterConstructor_ShouldReturnTheCloneOfTheObject()
        {
            message = new MockMessage(2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            MockMessage clone = (MockMessage)message.Clone();

            Console.WriteLine(message.ToString());

            // Assert.AreEqual(message.Id, clone.Id);
            // Assert.AreEqual(message.SenderId, clone.SenderId);
            // Assert.AreEqual(message.ReceiverId, clone.ReceiverId);
            // Assert.AreEqual(message.MessageContent, clone.MessageContent);
            // Assert.AreEqual(message.CommunicationDate, clone.CommunicationDate);
            Assert.AreEqual(message, clone);
        }
        [TestMethod]
        public void GetId_WhenInstantiatingWithFourParameterConstructor_ShouldReturnMinusOne()
        {
            message = new MockMessage(2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            Assert.AreEqual(-1, message.GetId());
        }
    }
}
