using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mikha;
using ISSProject.Common.Mikha.Controllers;
using ISSProject.Common.Mikha.Groups;
using ISSProject.Common.Mikha.Premium_Messages;
using ISSProject.Common.Mikha.Premium_Users;
using ISSProject.Common.Mock;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.SubscriptionServiceBackend.Post;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Messages;
using ISSProject_Regenerated.SubscriptionServiceBackend.Premium_Users;

namespace ISSProject_Regenerated.SubscriptionServiceBackend.Groups
{
    internal static class ContextAgnosticTester
    {
        public static Tuple<UserWrapper, UserWrapper> AddTestSubjects(IUserRepository userRepository, IPremiumUserRepository premiumUserRepository)
        {
            int sqlReflectedVanillaID = 107;
            int sqlReflectedPremiumID = 108;
            MockUser testPremium = new MockUser(sqlReflectedPremiumID, "t2337017n", "britney.willms@gmail.com", "Sherwood", "Sauer", DateTime.Now, "+40757458433");
            MockUser testVanilla = new MockUser(sqlReflectedVanillaID, "7hj427720a12c", "bernard.christiansen@hotmail.com", "Janice", "Klocko", DateTime.Now, "+40786649110");

            // userRepository.Insert(new UserWrapper(testVanilla));
            // userRepository.Insert(new UserWrapper(testPremium));
            // premiumUserRepository.Insert(new UserWrapper(testPremium));
            return new Tuple<UserWrapper, UserWrapper>(new UserWrapper(testPremium), new UserWrapper(testVanilla));
        }
        public static void MessageChecker(IUserRepository userRepository, IPremiumUserRepository premiumUserRepository, int sqlReflectedPremiumID, int sqlReflectedVanillaID)
        {
            IMessageRepository messageRepository = (IMessageRepository)new MessageRepository();
            IPremiumMessageRepository premiumMessageRepository = new PremiumMessageRepository();

            IPremiumMessageController premiumMessageController = new PremiumMessageController(premiumUserRepository, messageRepository, premiumMessageRepository);

            // MAKE SURE TO CHANGE THESE VALUES BEFORE ATTEMPTING THIS TEST
            int sqlReflectedPremiumMessageID = 119;
            int sqlReflectedVanillaMessageID = 120;
            MockMessage messagePremium = new MockMessage(sqlReflectedPremiumMessageID, sqlReflectedPremiumID, sqlReflectedVanillaID, "the boss can suck me", DateTime.Now);
            MockMessage messageVanilla = new MockMessage(sqlReflectedVanillaMessageID, sqlReflectedVanillaID, sqlReflectedPremiumID, "the boss can suck me too", DateTime.Now);

            bool succesfullyinsertedpremium = premiumMessageController.AddPremiumMessage(new MessageWrapper(messagePremium));
            bool succesfullyinsertedvanilla = premiumMessageController.AddPremiumMessage(new MessageWrapper(messageVanilla));
            bool succesfullydeletedpremium = premiumMessageController.DeletePremiumMessage(new MessageWrapper(messagePremium));
            bool succesfullydeletedvanilla = premiumMessageController.DeletePremiumMessage(new MessageWrapper(messageVanilla));

            Debug.Assert(succesfullydeletedpremium == true && succesfullyinsertedpremium == true);
            Debug.Assert(succesfullydeletedvanilla == false && succesfullyinsertedvanilla == false);
        }

        public static void GroupChecker(IUserRepository userRepository, IPremiumUserRepository premiumUserRepository, UserWrapper testPremium, UserWrapper testVanilla)
        {
            IMockGroupRepository mockGroupRepository = new MockGroupRepository();
            IGroupController groupController = new GroupController(premiumUserRepository, mockGroupRepository);

            bool noGroupsPremium = groupController.ExecuteSearch(testPremium, string.Empty).Count == 3;
            bool noGroupsVanilla = groupController.ExecuteSearch(testVanilla, string.Empty).Count == 2;

            Debug.Assert(noGroupsVanilla && noGroupsPremium);
        }

        public static void PostChecker(IUserRepository userRepository, IPremiumUserRepository premiumUserRepository, int sqlReflectedPremiumID, int sqlReflectedVanillaID)
        {
            IMockPostRepository mockPostRepository = new MockPostRepository();
            IPremiumPostRepository premiumPostRepository = new PremiumPostRepository();

            IPremiumPostController premiumPostController = new PremiumPostController(mockPostRepository, premiumPostRepository, premiumUserRepository);

            // MAKE SURE TO CHANGE THESE VALUES BEFORE ATTEMPTING THIS TEST
            int sqlReflectedPremiumPostID = 10;
            int sqlReflectedVanillaPostID = 11;
            MockPost postPremium = new MockPost(sqlReflectedPremiumPostID, sqlReflectedPremiumID, "ah", "lorem ipsum dolor sit amet", DateTime.Now);
            MockPost postVanilla = new MockPost(sqlReflectedVanillaPostID, sqlReflectedVanillaID, "ah too", "loremer ipsumer dolorer siter ameter", DateTime.Now);

            bool succesfullyinsertedpremium = premiumPostController.AddPremiumPost(postPremium);
            bool succesfullyinsertedvanilla = premiumPostController.AddPremiumPost(postVanilla);
            bool succesfullydeletedpremium = premiumPostController.DeletePremiumPost(postPremium);
            bool succesfullydeletedvanilla = premiumPostController.DeletePremiumPost(postVanilla);

            Debug.Assert(succesfullydeletedpremium == true && succesfullyinsertedpremium == true);
            Debug.Assert(succesfullydeletedvanilla == false && succesfullyinsertedvanilla == false);

            var priorityQueue = premiumPostController.GetPostQueue();

            while (priorityQueue.Count > 0)
            {
                var element = priorityQueue.Dequeue();
                Console.WriteLine(element);
            }
        }

        internal static void MessageChecker(UserRepository userRepository, PremiumUserRepository premiumUserRepository, int v1, int v2)
        {
            throw new NotImplementedException();
        }
    }
}
