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
using ISSProject_Regenerated.Mikha___SubscriptionServiceBackend;
namespace ISSProject.ScamBots
{
    internal class Program
    {
        private static void CreateBotThread()
        {
            FakeUserController controller = new FakeUserController();
            controller.WriteLogToConsole = true;
            Thread botThread = new Thread(new ThreadStart(controller.startBotMechanism));
            botThread.Start();
        }

        private static void AnalyseRandomUsers()
        {
            // reset DB
            MockFollowerRepository.ResetMockDatabase();

            // select some users for the analysing phase
            UserDiscordGraph graph = new UserDiscordGraph();
            graph.Verbose = true;

            graph.Users = UserRepository.Provided().All()
                                        .Where(user =>
                                            { // enjoy clearing up this "functional programming"
                                                return user.GetEmail() != "graph.traverser@system.ro";
                                            })
                                        .ToList();

            // instantiate user discord stuff
            UserDiscordController controller = new UserDiscordController(graph);
            controller.Verbose = true;

            // start async thread
            Thread thread = new Thread(new ThreadStart(() =>
                { // enjoy clearing up this "functional programming"
                    controller.TraverseAllUsers();
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
            Thread sTAThread = new Thread(new ThreadStart(() =>
            {
                SubscriptionServicePart.MainWindow main = new SubscriptionServicePart.MainWindow();
                main.Activate();
                main.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));

            sTAThread.SetApartmentState(ApartmentState.STA);
            sTAThread.Start();
        }

        private static void HandleMaliciousSubscriptionsFrontendViaThread()
        {
            Thread sTAThread = new Thread(new ThreadStart(() =>
            {
                GUICompanyForm.MainWindow main = new GUICompanyForm.MainWindow();
                main.Activate();
                main.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));

            sTAThread.SetApartmentState(ApartmentState.STA);
            sTAThread.Start();
        }

        private static void HandleSubscriptionServiceBackendOperations()
        {
            Console.WriteLine("HAVE YOU MODIFIED THE SQL IDS ? (Y/N)");
            char input = Console.ReadLine().ToLower().ToCharArray()[0];
            if (input == 'y')
            {
                UserRepository userRepository = new UserRepository();
                PremiumUserRepository premiumUserRepository = new PremiumUserRepository();
                Tuple<UserWrapper, UserWrapper> testSubjects = ContextAgnosticTester.AddTestSubjects(userRepository, premiumUserRepository);
                ContextAgnosticTester.MessageChecker(userRepository, premiumUserRepository, testSubjects.Item1.GetId(), testSubjects.Item2.GetId());
                ContextAgnosticTester.GroupChecker(userRepository, premiumUserRepository, testSubjects.Item1, testSubjects.Item2);
                ContextAgnosticTester.PostChecker(userRepository, premiumUserRepository, testSubjects.Item1.GetId(), testSubjects.Item2.GetId());
                Console.WriteLine("[+] PASSED");
            }
        }

        private static void HandleScamBotsPhishingFrontendViaThread()
        {
            Thread sTAThread = new Thread(new ThreadStart(() =>
            {
                Credit_card_donation.MainWindow main = new Credit_card_donation.MainWindow();
                main.Activate();
                main.Show();
                System.Windows.Threading.Dispatcher.Run();
            }));

            sTAThread.SetApartmentState(ApartmentState.STA);
            sTAThread.Start();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            // TESTS ARE NOT THREAD SAFE : RUN THIS BEFORE ANYTHING ELSE (OR NOT AT ALL)
            // MainTester.Test();

            // HandleMaliciousSubscriptionsBackendViaThread(); // Razvan - WORKS
            // createBotThread(); // Florin - WORKS
            // AnalyseRandomUsers(); // Rares - WORKS
            HandleSubscriptionServiceFrontendViaThread(); // Diana - WORKS
            // HandleMaliciousSubscriptionsFrontendViaThread(); // Nico - WORKS
            HandleSubscriptionServiceBackendOperations(); // Dragos - WORKS
            // HandleScamBotsPhishingFrontendViaThread(); // Dan - WORKS
            Console.ReadLine();
        }
    }
}
