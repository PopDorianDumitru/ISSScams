using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ISSProject.ScamBots;
using ISSProject.ScamBots.Model;

namespace ISSProject.Common.Test.ScamBots
{
    internal class ScamMessageGeneratorTest
    {
        public static void Test()
        {
            /// --- Preparing test data ---         ///

            List<string> templates = new List<string>() { "template1", "template2", "template3", "template4" };
            List<string> links = new List<string>() { "http://link1.com", "https://link2.org", "http://link3.co.uk", "http://link4.something" };
            List<ScamMessageTemplate> savedTemplates = new List<ScamMessageTemplate>();
            List<ScamMessageLink> savedLinks = new List<ScamMessageLink>();

            ScamMessageRepository scamMessageRepository = new ScamMessageRepository();
            foreach (string template in templates)
            {
                ScamMessageTemplate messageTemplate = new ScamMessageTemplate(template);
                Debug.Assert(scamMessageRepository.Insert(messageTemplate));
                messageTemplate.Id = scamMessageRepository.MessageIdByTemplate(messageTemplate.MessageContent);
                savedTemplates.Add(messageTemplate);
            }

            ScamMessageLinkRepository scamMessageLinkRepository = new ScamMessageLinkRepository();
            foreach (string link in links)
            {
                ScamMessageLink websiteLink = new ScamMessageLink(link);
                Debug.Assert(scamMessageLinkRepository.Insert(websiteLink));
                websiteLink.Id = scamMessageLinkRepository.LinkIdByUrl(websiteLink.LinkUrl);
                savedLinks.Add(websiteLink);
            }

            /// --- Actual testing ---         ///

            ScamMessageGenerator generator = new ScamMessageGenerator();
            Regex messagePattern = new Regex(@"\A.*https?:\/\/[a-zA-Z0-9-_]+(\.[a-zA-Z]+)+\Z");

            // test generation of one message
            Debug.Assert(messagePattern.IsMatch(generator.GenerateScamMessage()));

            // test generation of multiple messages
            List<string> generatedMessages = generator.GenerateScamMessages(100);

            foreach (string message in generatedMessages)
            {
                Debug.Assert(messagePattern.IsMatch(message));
            }

            /// --- Clean-up ---         ///

            foreach (ScamMessageTemplate template in savedTemplates)
            {
                Debug.Assert(scamMessageRepository.Delete(template));
            }

            foreach (ScamMessageLink link in savedLinks)
            {
                Debug.Assert(scamMessageLinkRepository.Delete(link));
            }
        }
    }
}
