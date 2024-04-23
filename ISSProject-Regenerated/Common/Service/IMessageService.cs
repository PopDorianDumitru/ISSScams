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
    internal interface IMessageService
    {
        private static IMessageService? singleton;
        public abstract static IMessageService Provided();

        /// <summary>
        /// Returns all users with whom this user has had conversations
        /// </summary>
        /// <param name="user">The wrapped user</param>
        /// <returns>The conversation targets of the wrapped user</returns>
        public IEnumerable<UserWrapper> GetConversationTargets(UserWrapper user);

        /// <summary>
        /// Returns the messages from sender to receiver.
        /// </summary>
        /// <param name="sender">The sender of the messages</param>
        /// <param name="receiver">The receiver of the messages</param>
        /// <returns>The messages from sender to receiver</returns>
        public IEnumerable<MessageWrapper> GetMessages(UserWrapper sender, UserWrapper receiver);
    }
}
