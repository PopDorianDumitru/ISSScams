using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject_Regenerated.ScamBots.Service;

namespace ISSProject.ScamBots
{
    internal class ScamMessageGenerator : IScamMessageGenerator
    {
        private readonly IScamMessageRepository messageTemplates;
        private readonly IScamMessageLinkRepository scamWebsiteLinks;
        private readonly Random randomGenerator;

        public ScamMessageGenerator()
        {
            randomGenerator = new Random();
            scamWebsiteLinks = new ScamMessageLinkRepository();
            messageTemplates = new ScamMessageRepository();
        }

        /// <summary>
        /// Returns a randomly generated scam message.
        /// </summary>
        /// <returns> A string containing a message template and an attached link to a malicious website.</returns>
        // random selection could be done in SQL, too
        public string GenerateScamMessage()
        {
            string messageTemplate = messageTemplates.GetMessageTemplateRandomly();
            string websiteLink = scamWebsiteLinks.GetMessageLinkRandomly();

            return messageTemplate + " " + websiteLink;
        }

        /// <summary>
        /// Returns a list of randomly generated scam messages.
        /// </summary>
        /// <param name="count">The number of entities to be generated</param>
        /// <returns> A list of strings, each containing a message template and an attached link to a malicious website.</returns>
        public List<string> GenerateScamMessages(int count)
        {
            List<string> messages = new List<string>();

            for (int i = 0; i < count; i++)
            {
                messages.Add(GenerateScamMessage());
            }

            return messages;
        }
    }
}
