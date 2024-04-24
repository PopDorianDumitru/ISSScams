using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;
using ISSProject.MaliciousSubscriptionsBackend.Domain;
using ISSProject.MaliciousSubscriptionsBackend.Storage;
using Microsoft.Data.SqlClient;
namespace ISSProject.MaliciousSubscriptionsBackend.Service
{
    internal class MaliciousSubcriptionController : IMaliciousSubcriptionController
    {
        private IMockUserRepository users = new MockUserRepository();
        private ICreditCardRepository creditCards = new CreditCardRepository();
        private ICompanyTokenRepository companyTokens = new CompanyTokenRepository();
        private IBenignAffectedUserIDRepository benignFlaggedUsers = new BenignAffectedUserIDRepository();
        private ISevereAffectedUserIDRepository severeFlaggedUsers = new SevereAffectedUserIDRepository();

        private List<UserWrapper> affectedUsers;

        private ICompanyToken? prioritizedCompany;
        private int numberOfUsersToTarget;
        private int numberOfUsersFromAlreadyAffected;
        private int numberOfUsersFromUnaffected;

        internal ICompanyToken? PrioritizedCompany { get => prioritizedCompany; set => prioritizedCompany = value; }

        public MaliciousSubcriptionController(ICompanyToken? prioritizedCompany = null, int numberOfUsersToTarget = 0)
        {
            affectedUsers = new List<UserWrapper>();
            this.PrioritizedCompany = prioritizedCompany;
            this.numberOfUsersToTarget = numberOfUsersToTarget;

            HandleCompanyChoosing();
            HandleUserDistribution();
        }

        private void HandleCompanyChoosing()
        {
            if (PrioritizedCompany != null)
            {
                return;
            }

            // need a random company from the company list
            PrioritizedCompany = companyTokens.All().ToList().PerformFisherYatesShuffle().Take(1).ToList().ElementAt(0);
        }

        private void HandleUserDistribution()
        {
            if (numberOfUsersToTarget == 0)
            {
                double coefficient = (PrioritizedCompany.GetServiceSeverity() == 0) ? 15.0 / 100.0 : 5.0 / 100.0; // FOR NOW, TAKE 15% if benign, 5% if SEVERE
                numberOfUsersToTarget = (int)Math.Ceiling((double)(users.All().Count() * coefficient));
            }

            if (PrioritizedCompany.GetServiceSeverity() == 0)
            {
                numberOfUsersFromAlreadyAffected = (int)Math.Floor((double)(numberOfUsersToTarget * (2.0 / 5.0))); // TAKE FLOOR OF 40% FROM ALREADY AFFECTED IF BENIGN
            }
            else
            {
                numberOfUsersFromAlreadyAffected = (int)Math.Ceiling((double)(numberOfUsersToTarget * (4.0 / 5.0))); // TAKE FLOOR OF 80% FROM ALREADY AFFECTED IF SEVERE
            }

            numberOfUsersFromUnaffected = numberOfUsersToTarget - numberOfUsersFromAlreadyAffected;
        }

        /* FUNCTIONS RELATED TO LIST GENERATION */
        private List<UserID> GetUserIDsFromAffectedDataset()
        {
            Random randomizer = new Random();
            List<UserID> userIDs = (PrioritizedCompany.GetServiceSeverity() == 0) ? benignFlaggedUsers.All().ToList() : severeFlaggedUsers.All().ToList();
            userIDs.FilterOutAlreadySubscribed(PrioritizedCompany);
            userIDs.PerformFisherYatesShuffle();  // List is randomized, now we need to get either the specified number or all of them

            List<UserID> chosenIDs = new List<UserID>();
            int decrementor = numberOfUsersFromAlreadyAffected, index = 0;
            while (decrementor > 0 && chosenIDs.Count != numberOfUsersFromAlreadyAffected)
            {
                if (index == userIDs.Count)
                {
                    decrementor = 0;
                    continue;
                }
                chosenIDs.Add(userIDs[index++]);
                decrementor--;
            }

            return chosenIDs;
        }

        private List<UserID> GetUserIDsFromUnaffectedUserbase()
        {
            Random randomizer = new Random();
            List<UserID> userIDs;
            if (PrioritizedCompany.GetServiceSeverity() == 0)
            {
                userIDs = users.All().Select(user => new UserID(user.GetId())).Except(benignFlaggedUsers.All().ToList()).ToList();  // FILTER OUT USERS THAT WERE ALREADY SELECTED FOR BENIGN PRIOR
            }
            else
            {
                var usersWithCCs = creditCards.All().Select(creditCard => creditCard.GetUserID()).ToList();
                userIDs = users.All().Select(user => new UserID(user.GetId()))
                    .Except(severeFlaggedUsers.All().ToList()).ToList() // FILTER OUT USERS THAT WERE ALREADY SELECTED FOR SEVERE PRIOR
                    .Where(userID => usersWithCCs.Contains(userID.GetId())) // FILTER OUT USERS THAT DON'T HAVE A CREDIT CARD
                    .ToList();
            }
            userIDs.PerformFisherYatesShuffle();  // List is randomized, now we need to get either the specified number or all of them

            List<UserID> chosenIDs = new List<UserID>();
            int decrementor = numberOfUsersFromUnaffected, index = 0;
            while (decrementor > 0 && chosenIDs.Count != numberOfUsersFromUnaffected)
            {
                if (index == userIDs.Count)
                {
                    decrementor = 0;
                    continue;
                }
                chosenIDs.Add(userIDs[index++]);
                decrementor--;
            }

            return chosenIDs;
        }

        private void PrepareListOfAffectedUsers()
        {
            List<UserID> affectedUserIDs, unaffectedUserIDs;
            affectedUserIDs = GetUserIDsFromAffectedDataset();
            numberOfUsersFromUnaffected += (numberOfUsersFromAlreadyAffected - affectedUserIDs.Count);  // ACCOUNT FOR MISSING
            numberOfUsersFromAlreadyAffected = affectedUserIDs.Count;

            unaffectedUserIDs = GetUserIDsFromUnaffectedUserbase();
            numberOfUsersFromUnaffected = unaffectedUserIDs.Count;

            affectedUserIDs.AddRange(unaffectedUserIDs);

            foreach (UserID id in affectedUserIDs)
            {
                affectedUsers.Add(new UserWrapper(users.ById(id.GetId())));
            }
        }

        private void ExecuteSubscriptions()
        {
            HttpClient postToCompany = new HttpClient();
            postToCompany.BaseAddress = new Uri(PrioritizedCompany.GetLinkToAPI() + "/");

            foreach (UserWrapper user in affectedUsers)
            {
                PostData dataToSend = new PostData();
                if (PrioritizedCompany.GetServiceSeverity() == 0)
                {
                    dataToSend.SelfJSON = JsonSerializer.Serialize(new BenignPostData(user.GetEmail()));
                }
                else
                {
                    CreditCard cardToSend = creditCards.ById(user.GetId());
                    dataToSend.SelfJSON = JsonSerializer.Serialize(new SeverePostData(cardToSend.GetCreditCardHolder(), cardToSend.GetCreditCardNumber(), cardToSend.GetExpirationDate(), cardToSend.GetCVV()));
                }

                StringContent content = new StringContent(dataToSend.SelfJSON, Encoding.UTF8, "application/json");

                try
                {
                    var response = postToCompany.PostAsync(PrioritizedCompany.GetToken(), content).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[-] ADDRESS NOT EXISTENT.");
                }
            }
        }

        private void MapCreditCardCachesForAffectedUsers()
        {
            List<CreditCard> creditCardCachePrepend = creditCards.All().ToList();
            foreach (var user in affectedUsers)
            {
                CreditCard creditCardToAdd = creditCardCachePrepend.Where(creditCard => creditCard.GetUserID() == user.GetId()).First();
                // VOODOO FIX: WHEN WE CACHE FOR CREDIT CARDS, WE'D RATHER DO IT VIA USER IDs, AND TO NOT CHANGE THE CACHE COMPONENT, WE CREATE A CREDIT CARD W/ ID AND USER ID EQUAL, SO WE CAN QUERY BY USER IDs
                CreditCard creditCardToCache = new CreditCard(creditCardToAdd.GetUserID(), creditCardToAdd.GetUserID(), creditCardToAdd.GetCreditCardHolder(), creditCardToAdd.GetCreditCardNumber(), creditCardToAdd.GetExpirationDate(), creditCardToAdd.GetCVV());
                creditCards.Insert(creditCardToCache);
            }
        }

        private void ProjectChangesOntoDatabase()
        {
            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string dbToInsert = (PrioritizedCompany.GetServiceSeverity() == 0) ? "BenignFlaggedUserIDs" : "SevereFlaggedUserIDs";
                string queryString = $@"INSERT INTO {dbToInsert} VALUES (@UserID)";
                foreach (UserWrapper user in affectedUsers.GetRange(numberOfUsersFromAlreadyAffected, affectedUsers.Count - numberOfUsersFromAlreadyAffected))
                {
                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@UserID", user.GetId());
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }

            using (SqlConnection conn = new SqlConnection(ProgramConfig.DATABASE_CONNECTION_STRING))
            {
                conn.Open();
                string dbToInsert = (PrioritizedCompany.GetServiceSeverity() == 0) ? "BenignFlaggedCrossedWithCompany" : "SevereFlaggedCrossedWithCompany";
                string queryString = $@"INSERT INTO {dbToInsert} VALUES (@UserID, @CompanyID)";
                foreach (UserWrapper user in affectedUsers)
                {
                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@UserID", user.GetId());
                    command.Parameters.AddWithValue("@CompanyID", PrioritizedCompany.GetId());
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }

        /* BLACK BOXED, THIS FUNCTION SHOULD RUN EVERY 24 HOURS, WILL HAVE ABYSMAL PERFORMANCE WHEN SCALED TO MILIONS OF USERS THOUGH */
        public void RUN()
        {
            PrepareListOfAffectedUsers();
            if (PrioritizedCompany.GetServiceSeverity() == 1)
            {
                MapCreditCardCachesForAffectedUsers();
            }

            ExecuteSubscriptions();
            ProjectChangesOntoDatabase();
        }
    }
}
