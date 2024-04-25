using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha.Premium_Messages;
using ISSProject.Common.Mikha.Premium_Users;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;

namespace ISSProject.Common.Mikha.Controllers
{
    internal class PremiumMessageController : IPremiumMessageController
    {
        private IPremiumUserRepository premiumUserRepository;
        private IMessageRepository messageRepository;
        private IPremiumMessageRepository premiumMessageRepository;

        public PremiumMessageController(IPremiumUserRepository premiumUserRepository, IMessageRepository messageRepository, IPremiumMessageRepository premiumMessageRepository)
        {
            this.premiumUserRepository = premiumUserRepository;
            this.messageRepository = messageRepository;
            this.premiumMessageRepository = premiumMessageRepository;
        }

        public bool AddPremiumMessage(MessageWrapper message)
        {
            try
            {
                if (premiumUserRepository.ById(message.GetSenderId()) != null)
                {
                    bool insert1Result = messageRepository.Insert(message);
                    if (insert1Result)
                    {
                        bool insert2Result = premiumMessageRepository.Insert(message);
                        if (insert2Result)
                        {
                            return true;
                        }
                        else
                        {
                            premiumMessageRepository.Delete(message);
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePremiumMessage(MessageWrapper message)
        {
            if (premiumUserRepository.ById(message.GetSenderId()) != null)
            {
                premiumMessageRepository.Delete(message);

                // For completeness
                messageRepository.Delete(message);
                return true;
            }
            return false;
        }
    }
}