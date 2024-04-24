using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using ISSProject.Common.Mock;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;
using ISSProject.ScamBots.Model;
using ISSProject_Regenerated.Common.Test;

namespace ISSProject.Common.Test.ScamBots
{
    internal class ScamMessageRepositoryTest : ITest
    {
        public static void Test()
        {
            ScamMessageRepository scamMessageRepository = new ScamMessageRepository();
            ScamMessageTemplate messageTemplate = new ScamMessageTemplate("Hello! My name is John and I work for UPS Global Shipping and Logistics Services. We have a package delivery pending for one of your orders, and we need some additional from you in order to make the delivery. Please check out the following link: ");
            int initialSize = scamMessageRepository.Size();

            // get message template by id that doesn't exist
            try
            {
                scamMessageRepository.ById(0);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            // insert message template into the database
            Debug.Assert(scamMessageRepository.Insert(messageTemplate));
            Debug.Assert(scamMessageRepository.Size() == initialSize + 1);

            // retrieve assigned id
            messageTemplate.Id = scamMessageRepository.MessageIdByTemplate(messageTemplate.MessageContent);

            // retrieve message template by its assigned id
            ScamMessageTemplate result = scamMessageRepository.ById(messageTemplate.Id);
            Debug.Assert(result.Id.Equals(messageTemplate.Id));
            Debug.Assert(result.MessageContent.Equals(messageTemplate.MessageContent));

            // update message template in the database
            messageTemplate.MessageContent = "This template has been modified to remove some obvious grammar mistakes :)";
            Debug.Assert(scamMessageRepository.Update(messageTemplate));

            // retrieve message template from database and check if the changes persist
            Debug.Assert(scamMessageRepository.ById(messageTemplate.Id).MessageContent.Equals("This template has been modified to remove some obvious grammar mistakes :)"));

            // delete message template from the database
            Debug.Assert(scamMessageRepository.Delete(messageTemplate));
            Debug.Assert(scamMessageRepository.Size() == initialSize);

            // check that the message template is no longer in the database
            try
            {
                scamMessageRepository.ById(messageTemplate.Id);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }
        }
    }
}
