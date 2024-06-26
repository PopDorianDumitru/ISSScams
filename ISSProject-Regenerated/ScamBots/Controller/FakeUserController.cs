﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject.Common.Logging;
using ISSProject.Common.Mock;
using ISSProject.ScamBots.Service;
using ISSProject_Regenerated.ScamBots.Controller;
using ISSProject_Regenerated.ScamBots.Service;
using Microsoft.Data.SqlClient;
namespace ISSProject.ScamBots
{
    internal class FakeUserController : IFakeUserController
    {
        private IFakeUserRepository fakeUsers;
        private ISizedRepository<MockUser, int> allUsers;
        private ISizedRepository<MockMessage, int> messageRepository;

        private IScamMessageGenerator templateMessages;
        private IFakeUserGenerator fakeUserGenerator;

        private ILoggingModule logger = new LoggingModule("scambots.txt", "ScamBot");
        public bool WriteLogToConsole
        {
            get { return logger.WriteToConsole; } set { logger.WriteToConsole = value; }
        }

        private int populationSizePercentage;

        private int messagesPerBot;
        private int attackWaveCooldownInHours;

        /// <summary>
        /// Initializes a new instance of the controller for the scam bot account mechanism.
        /// <param name="attackWaveCooldownInHours">attack wave cooldown (in hours)</param>
        /// <param name="messagesPerBot">number of messages sent per wave by each bot</param>
        /// <param name="populationPercentage">number of bots as percentage of the legitimate userbase population</param>
        /// </summary>
        public FakeUserController(int attackWaveCooldownInHours, int messagesPerBot, int populationPercentage)
        {
            templateMessages = new ScamMessageGenerator();
            fakeUserGenerator = new FakeUserGenerator();
            messageRepository = new MockMessageRepository();
            fakeUsers = new FakeUserRepository();

            this.attackWaveCooldownInHours = attackWaveCooldownInHours;
            this.messagesPerBot = messagesPerBot;
            this.populationSizePercentage = populationPercentage;
        }

        /// <summary>
        /// Initializes a new instance of the controller for the scam bot account mechanism, with the following values: <br/>
        /// - attack wave cooldown (in hours) : 24 <br/>
        /// - number of messages sent per wave by each bot : 2 <br/>
        /// - percentage of targeted legitimate userbase: 10% <br/>
        /// - number of bots as percentage of the legitimate userbase population : 5% <br/>
        /// </summary>
        public FakeUserController()
        {
            templateMessages = new ScamMessageGenerator();
            fakeUserGenerator = new FakeUserGenerator();
            messageRepository = new MockMessageRepository();
            fakeUsers = new FakeUserRepository();

            this.attackWaveCooldownInHours = 24;
            this.messagesPerBot = 2;
            this.populationSizePercentage = 5;
        }

        /// <summary>
        /// Starts the scam bot mechanism on the current thread. Pass this as a worker to a thread object.
        /// Will call startAttackWave
        /// </summary>
        public void StartBotMechanism()
        {
            logger.Log(LogSeverity.Event, "Starting scam bots thread with the following parameters:");
            logger.Log(LogSeverity.Info, "MESSAGES_PER_BOT = " + messagesPerBot + " | ATTACK_WAVE_COOLDOWN = " + attackWaveCooldownInHours + " HOURS | BOT_POPULATION_PERCENTAGE = " + populationSizePercentage + ".");

            try
            {
                while (true)
                {
                    logger.Log(LogSeverity.Event, "Starting a new scam bots attack wave...");
                    StartAttackWave();
                    logger.Log(LogSeverity.Success, "Attack wave finished! Thread will sleep for " + attackWaveCooldownInHours + " hours.");
                    Thread.Sleep(attackWaveCooldownInHours * 3600 * 1000);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogSeverity.Error, "An error occured in the scam bots thread, shutting it down.");
                logger.Log(LogSeverity.Error, "Error message: " + ex.Message);
            }
        }

        /// <summary>
        /// Starts the attack wave. All bots will be activated, and the following will happen: <br/>
        /// - erase all bot accounts that have been banned <br/>
        /// - generated new ones until the established threshold is met <br/>
        /// - generate a set number of scam messages for each bot and send them to legitimate users
        /// </summary>
        /// <returns></returns>
        public void StartAttackWave()
        {
            int result = 0;

            logger.Log(LogSeverity.Event, "Generating new accounts...");
            result = GenerateBotAccounts();
            logger.Log(LogSeverity.Info, "Generated " + result + " new bot accounts.");

            logger.Log(LogSeverity.Event, "Sending scam messages to users...");
            result = SendScamMessages();
            logger.Log(LogSeverity.Info, "Sent " + result + " messages from bots.");
        }

        /// <summary>
        /// [NO LONGER USED] Erases all the banned bot accounts from the fake account database.
        /// </summary>
        /// <returns>The number of banned bot accounts that have been erased.</returns>
        public static int RemoveBannedBotAccounts()
        {
            return FakeUserRepository.RemoveBannedBotAccounts();
        }

        /// <summary>
        /// Creates and inserts fake accounts into the database, such that the total number of active fake accounts is equal to the specified population size(default is 5% of total legitimate userbase).
        /// </summary>
        /// <returns>The number of generated accounts.</returns>
        public int GenerateBotAccounts()
        {
            long numberOfBotAccounts = fakeUsers.Size();
            long numberOfLegitimateAccounts = new MockUserRepository().Size() - fakeUsers.Size() - fakeUsers.NumberOfBannedFakeAccounts();
            logger.Log(LogSeverity.Warning, "Number of legit accounts: " + numberOfLegitimateAccounts);

            int generatedAccountsCount = 0;
            long desiredNumberOfAccounts = 5 * numberOfLegitimateAccounts / 100;
            int attempts = 0;

            for (long i = numberOfBotAccounts; i < desiredNumberOfAccounts; i++)
            {
                try
                {
                    fakeUsers.Insert(fakeUserGenerator.GenerateFakeUser());
                    generatedAccountsCount++;
                }
                catch (Exception ex)
                {
                    attempts++;
                    i--;

                    if (attempts > 3)
                    {
                        throw new FakeUserControllerException("Failed user generation after 3rd attempt!");
                    }

                    logger.Log(LogSeverity.Warning, "[ATTEMPT " + attempts + "] Failed to generate a new account, trying again...");
                }
            }

            return generatedAccountsCount;
        }

        /// <summary>
        /// Sends scam messages from all the bots to legitimate users. Each bot will try to send only one message for every unique user. If there are no users left to send the messages to, the algorithm stops.
        /// </summary>
        /// <returns></returns>
        public int SendScamMessages()
        {
            return FakeUserRepository.SendScamMessages(messagesPerBot, populationSizePercentage, messageRepository, templateMessages);
        }
    }
}
