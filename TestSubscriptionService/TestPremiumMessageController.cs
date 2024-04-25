using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha.Controllers;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;

namespace TestSubscriptionService
{
    [TestClass]
    public class TestPremiumMessageController
    {
        private IPremiumUserRepository premiumUserRepository;
        private IMessageRepository messageRepository;
        private IPremiumMessageRepository premiumMessageRepository;
        private IPremiumMessageController premiumMessageController;

        [TestInitialize]
        public void TestInitializer()
        {
            premiumUserRepository = new PremiumUserInMemoryRepository();
            messageRepository = new MessageRepository();
            premiumMessageRepository = new PremiumMessageInMemoryRepository();

            UserWrapper premiumUser = new UserWrapper(1);
            premiumUserRepository.Insert(premiumUser);

            

            premiumMessageController = new PremiumMessageController(premiumUserRepository, messageRepository, premiumMessageRepository);

           // premiumUserRepository = new PremiumUserInMemoryRepository();
           // messageRepository = new Message;
        }
    }
}
