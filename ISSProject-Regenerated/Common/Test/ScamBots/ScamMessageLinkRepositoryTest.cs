using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;
using ISSProject.ScamBots.Model;

namespace ISSProject.Common.Test.ScamBots
{
    internal class ScamMessageLinkRepositoryTest
    {
        public static void Test()
        {
            ScamMessageLinkRepository scamMessageLinkRepository = new ScamMessageLinkRepository();
            ScamMessageLink websiteLink = new ScamMessageLink("https://www.our-scam-website.scam");
            int initialSize = scamMessageLinkRepository.Size();

            // get website link by id that doesn't exist
            try
            {
                scamMessageLinkRepository.ById(0);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            // insert website link into the database
            Debug.Assert(scamMessageLinkRepository.Insert(websiteLink));
            Debug.Assert(scamMessageLinkRepository.Size() == initialSize + 1);

            // retrieve assigned id
            websiteLink.Id = scamMessageLinkRepository.LinkIdByUrl(websiteLink.LinkUrl);

            // retrieve website link by its assigned id
            ScamMessageLink result = scamMessageLinkRepository.ById(websiteLink.Id);
            Debug.Assert(result.Id == websiteLink.Id);
            Debug.Assert(result.LinkUrl == websiteLink.LinkUrl);

            // update website link in the database
            websiteLink.LinkUrl = "https://www.our-new-scam-website.newscam";
            Debug.Assert(scamMessageLinkRepository.Update(websiteLink));

            // retrieve website link from database and check if the changes persist
            Debug.Assert(scamMessageLinkRepository.ById(websiteLink.Id).LinkUrl.Equals("https://www.our-new-scam-website.newscam"));

            // delete website link from the database
            Debug.Assert(scamMessageLinkRepository.Delete(websiteLink));
            Debug.Assert(scamMessageLinkRepository.Size() == initialSize);

            // check that the website link is no longer in the database
            try
            {
                scamMessageLinkRepository.ById(websiteLink.Id);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }
        }
    }
}
