using ISSProject.Common.Mikha;
using ISSProject.Common.Mikha.Controllers;
using ISSProject.Common.Mikha.Premium_Messages;
using ISSProject.Common.Mikha.Premium_Users;
using ISSProject.Common.Mock;
using ISSProject.Common.Repository;
using ISSProject.Common.Test;
using ISSProject.Common.Wrapper;
using ISSProject.GraphAnalyser.Controller;
using ISSProject.GraphAnalyser.Domain;
using ISSProject.MaliciousSubscriptionsBackend;
using ISSProject.MaliciousSubscriptionsBackend.Storage;
using ISSProject_Regenerated.SubscriptionServiceBackend.Controllers;
using ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards;
using ISSProject_Regenerated.SubscriptionServiceBackend.Groups;
using SubscriptionServicePart.MVVM.ViewModel;
namespace ISSProject.ScamBots
{
    internal class Program
    {
        private static void CreateBotThread()
        {
            FakeUserController fakeUserController = new FakeUserController();
            fakeUserController.WriteLogToConsole = true;
            Thread botThread = new Thread(new ThreadStart(fakeUserController.StartBotMechanism));
            botThread.Start();
        }

        private static void AnalyseRandomUsers()
        {
            // reset DB
            MockFollowerRepository.ResetMockDatabase();

            // select some users for the analysing phase
            UserDiscordGraph undiscoveredGraph = new UserDiscordGraph();
            undiscoveredGraph.Verbose = true;

            undiscoveredGraph.Users = UserRepository.Provided().All()
                                        .Where(user =>
                                            { // enjoy clearing up this "functional programming"
                                                return user.GetEmail() != "graph.traverser@system.ro";
                                            })
                                        .ToList();

            // instantiate user discord stuff
            UserDiscordController userDiscordController = new UserDiscordController(undiscoveredGraph);
            userDiscordController.Verbose = true;

            // start async thread
            Thread thread = new Thread(new ThreadStart(() =>
                { // enjoy clearing up this "functional programming"
                    userDiscordController.TraverseAllUsers();
                    Thread.Sleep(3600 * 1000); // re-analyse in 1 hour
                }));
            thread.Start();
        }

        private static void HandleMaliciousSubscriptionsBackendViaThread()
        {
            Driver.RUN();
        }

        private static void HandleSubscriptionServiceFrontendViaThread()
        {
            Thread statThread = new Thread(new ThreadStart(() =>
            {
                ISSProject_Regenerated.SubscriptionServiceBackend.CreditCards.ICreditCardRepository creditCardRepository = new CreditCardInMemoryRepository();
                ICreditCardController creditCardController = new CreditCardController(creditCardRepository);
                ICreditCardValidatorService creditCardValidatorService = new CreditCardValidatorService();
                MainViewModel mainViewModel = new MainViewModel(creditCardController, creditCardValidatorService);
                SubscriptionServicePart.MainWindow subscriptionServiceMainWindow = new SubscriptionServicePart.MainWindow(mainViewModel);
                subscriptionServiceMainWindow.Activate();
                subscriptionServiceMainWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));

            statThread.SetApartmentState(ApartmentState.STA);
            statThread.Start();
        }

        private static void HandleMaliciousSubscriptionsFrontendViaThread()
        {
            Thread sTAThread = new Thread(new ThreadStart(() =>
            {
                GUICompanyForm.MainWindow guiCompanyFormMainWindow = new GUICompanyForm.MainWindow();
                guiCompanyFormMainWindow.Activate();
                guiCompanyFormMainWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));

            sTAThread.SetApartmentState(ApartmentState.STA);
            sTAThread.Start();
        }

        // private static void HandleSubscriptionServiceBackendOperations()
        // {
        //    Console.WriteLine("HAVE YOU MODIFIED THE SQL IDS ? (Y/N)");
        //    char userAgrees = Console.ReadLine().ToLower().ToCharArray()[0];
        //    if (userAgrees == 'y')
        //    {
        //        UserRepository userRepository = new UserRepository();
        //        PremiumUserRepository premiumUserRepository = new PremiumUserRepository();
        //        Tuple<UserWrapper, UserWrapper> testSubjects = ContextAgnosticTester.AddTestSubjects((IUserRepository)userRepository, premiumUserRepository);
        //        ContextAgnosticTester.MessageChecker((IUserRepository)userRepository, premiumUserRepository, testSubjects.Item1.GetId(), testSubjects.Item2.GetId());
        //        ContextAgnosticTester.GroupChecker((IUserRepository)userRepository, premiumUserRepository, testSubjects.Item1, testSubjects.Item2);
        //        ContextAgnosticTester.PostChecker((IUserRepository)userRepository, premiumUserRepository, testSubjects.Item1.GetId(), testSubjects.Item2.GetId());
        //        Console.WriteLine("[+] PASSED");
        //    }
        // }
        private static void HandleScamBotsPhishingFrontendViaThread()
        {
            Thread statThread = new Thread(new ThreadStart(() =>
            {
                Credit_card_donation.MainWindow creditCardDonationMainWindow = new Credit_card_donation.MainWindow();
                creditCardDonationMainWindow.Activate();
                creditCardDonationMainWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));

            statThread.SetApartmentState(ApartmentState.STA);
            statThread.Start();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            // TESTS ARE NOT THREAD SAFE : RUN THIS BEFORE ANYTHING ELSE (OR NOT AT ALL)
            // MainTester.Test();
            // HandleMaliciousSubscriptionsBackendViaThread(); // Razvan - WORKS
            // CreateBotThread(); // Florin - WORKS
            // AnalyseRandomUsers(); // Rares - WORKS
            HandleSubscriptionServiceFrontendViaThread(); // Diana - WORKS
            // HandleMaliciousSubscriptionsFrontendViaThread(); // Nico - WORKS
            // HandleSubscriptionServiceBackendOperations(); // Dragos - WORKS
            // HandleScamBotsPhishingFrontendViaThread(); // Dan - WORKS
            Console.ReadLine();
        }
    }
}
