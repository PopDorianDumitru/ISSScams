using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.Common.Repository;
using ISSProject.Common.Service;
using ISSProject.Common.Wrapper;

namespace ISSfixed.ISSProject.Common.Service
{
    internal class MessageService
    {
        private static MessageService? singleton;
        public static MessageService Provided()
        {
            if (singleton == null)
            {
                singleton = new MessageService();
            }

            return singleton;
        }

        /// <summary>
        /// Returns all users with whom this user has had conversations
        /// </summary>
        /// <param name="user">The wrapped user</param>
        /// <returns>The conversation targets of the wrapped user</returns>
        public IEnumerable<UserWrapper> GetConversationTargets(UserWrapper user)
        {
            return MessageRepository.Provided().GetReceiversOfSender(user);
        }

        /// <summary>
        /// Returns the messages from sender to receiver.
        /// </summary>
        /// <param name="sender">The sender of the messages</param>
        /// <param name="receiver">The receiver of the messages</param>
        /// <returns>The messages from sender to receiver</returns>
        public IEnumerable<MessageWrapper> GetMessages(UserWrapper sender, UserWrapper receiver)
        {
            return MessageRepository.Provided().GetMessages(sender, receiver);
        }
    }
}
