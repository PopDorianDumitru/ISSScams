using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ISSProject.Common.Mock;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;

namespace ISSProject.Common.Test.Common
{
    internal class MessageRepositoryTest
    {
        public static void Test()
        {
            MockUserRepository userRepository = new MockUserRepository();
            MockMessageRepository messageRepository = new MockMessageRepository();
            MockUser user1 = new MockUser("fakepass", "an@addres.email", "Jean", "Baptiste", new SqlDateTime(DateTime.Now).Value, "1770077077");
            MockUser user2 = new MockUser("fakepass", "an@addres2.email", "Jean", "Baptiste", new SqlDateTime(DateTime.Now).Value, "1770077079");
            int initialSize = messageRepository.Size();

            // insert test users into the database
            Debug.Assert(userRepository.Insert(user1));
            Debug.Assert(userRepository.Insert(user2));

            user1.Id = MockUserRepository.GetUserIdByEmail(user1.Email);
            user2.Id = MockUserRepository.GetUserIdByEmail(user2.Email);

            // get message by id that doesn't exist
            try
            {
                messageRepository.ById(0);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            DateTime messageDateTime = new SqlDateTime(DateTime.Now).Value;
            MockMessage testMessage = new MockMessage(user1.Id, user2.Id, "Hello! This is a test message.", messageDateTime);

            // insert message into the database
            Debug.Assert(messageRepository.Insert(testMessage));
            Debug.Assert(messageRepository.Size() == initialSize + 1);

            // retrieve message by its assigned id
            testMessage.Id = MockMessageRepository.GetMessageIdByConversationStatus(user1.Id, user2.Id, messageDateTime);
            MockMessage result = messageRepository.ById(testMessage.Id);
            Debug.Assert(result.SenderId == testMessage.SenderId);
            Debug.Assert(result.ReceiverId == testMessage.ReceiverId);
            Debug.Assert(result.Id == testMessage.Id);
            Debug.Assert(result.MessageContent == testMessage.MessageContent);
            Debug.Assert(result.CommunicationDate == testMessage.CommunicationDate);

            // update message in the database
            testMessage.MessageContent = "This message has been edited by the sender!";
            Debug.Assert(messageRepository.Update(testMessage));

            // retrieve user from database and check if the changes persist
            Debug.Assert(messageRepository.ById(testMessage.Id).MessageContent == "This message has been edited by the sender!");

            // delete message from the database
            Debug.Assert(messageRepository.Delete(testMessage));
            Debug.Assert(messageRepository.Size() == initialSize);

            // check that the user is no longer in the database
            try
            {
                messageRepository.ById(testMessage.Id);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            /// --- Clean-up ----       ///

            // delete users from the database
            Debug.Assert(userRepository.Delete(user1));
            Debug.Assert(userRepository.Delete(user2));

            // check that test users are no longer in the database
            try
            {
                userRepository.ById(user1.Id);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }
            try
            {
                userRepository.ById(user2.Id);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }
        }
    }
}
