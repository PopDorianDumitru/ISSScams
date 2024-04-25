using ISSProject.Common.Mikha.Controllers;
using ISSProject.Common.Repository;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

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
           // premiumUserRepository = new PremiumUserInMemoryRepository();
           // messageRepository = new Message;

        }
    }
}
