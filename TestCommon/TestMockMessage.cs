﻿using ISSProject.Common.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCommon
{
    [TestClass]
    public class TestMockMessage
    {

        private MockMessage message;

        [TestMethod]
        public void Clone_ConstructorWithFiveParameters_ShouldReturnTheCloneOfTheObject()
        {
            message = new MockMessage(1, 2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            MockMessage clone = (MockMessage)message.Clone();

            Assert.AreEqual(message.Id, clone.Id);
            Assert.AreEqual(message.SenderId, clone.SenderId);
            Assert.AreEqual(message.ReceiverId, clone.ReceiverId);
            Assert.AreEqual(message.MessageContent, clone.MessageContent);
            Assert.AreEqual(message.CommunicationDate, clone.CommunicationDate);
        }

        [TestMethod]
        public void Clone_ConstructorWithFourParameters_ShouldReturnTheCloneOfTheObject()
        {
            message = new MockMessage(2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            MockMessage clone = (MockMessage)message.Clone();

            Assert.AreEqual(message.Id, clone.Id);
            Assert.AreEqual(message.SenderId, clone.SenderId);
            Assert.AreEqual(message.ReceiverId, clone.ReceiverId);
            Assert.AreEqual(message.MessageContent, clone.MessageContent);
            Assert.AreEqual(message.CommunicationDate, clone.CommunicationDate);
        }

        [TestMethod]

        public void GetId_ConstructorWithFiveParameters_TestMessageGetId()
        {
            message = new MockMessage(1, 2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            Assert.AreEqual(1, message.GetId());
        }

        [TestMethod]

        public void GetId_ConstructorWithFourParameters_TestMessageGetId()
        {
            message = new MockMessage(2, 3, "Hello", new DateTime(2022, 4, 26, 10, 30, 0));

            Assert.AreEqual(-1, message.GetId());
        }
    }
}