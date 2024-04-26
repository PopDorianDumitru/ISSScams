using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using ISSProject;
using ISSProject.Common.DataEncryption;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;
using ISSProject_Regenerated.ScamBots.Service;

namespace ISSProject.ScamBots.Service
{
    internal class FakeUserGenerator : IFakeUserGenerator
    {
        private readonly List<string> fakeFirstNames;
        private readonly List<string> fakeLastNames;
        private readonly Random randomGenerator;
        private readonly ShiftEncrypter encrypter1;
        private readonly ShiftEncrypter encrypter2;

        public FakeUserGenerator()
        {
            randomGenerator = new Random();
            encrypter1 = new ShiftEncrypter(5);
            encrypter2 = new ShiftEncrypter(15);

            string filePath = "C:\\Users\\doria\\source\\repos\\ISSScams\\ISSProject-Regenerated\\ScamBots\\first_names.txt";
            fakeFirstNames = File.ReadAllLines(filePath).ToList();

            filePath = "C:\\Users\\doria\\source\\repos\\ISSScams\\ISSProject-Regenerated\\ScamBots\\last_names.txt";
            fakeLastNames = File.ReadAllLines(filePath).ToList();
        }

        /// <summary>
        /// Randomly generates a user with its associated properties.
        /// </summary>
        /// <returns> a randomly generated user</returns>
        public MockUser GenerateFakeUser()
        {
            string firstName, lastName, email, phoneNumber, password;

            firstName = fakeFirstNames[randomGenerator.Next(1, fakeFirstNames.Count + 1) - 1];
            lastName = fakeLastNames[randomGenerator.Next(1, fakeLastNames.Count + 1) - 1];
            phoneNumber = "22";

            for (int index = 0; index < 8; ++index)
            {
                phoneNumber += (randomGenerator.Next(1, 11) - 1).ToString();
            }

            int randomCode;
            if (randomGenerator.Next() % 2 == 1)
            {
                randomCode = randomGenerator.Next(10000, 100000);
            }
            else
            {
                randomCode = randomGenerator.Next(100000, 1000000);
            }

            email = firstName.ToLower() + "_" + lastName.ToLower() + "_" + randomCode + "_" + phoneNumber + "@gmail.com";

            password = encrypter1.EncryptString(firstName) + "_" + encrypter2.EncryptString(lastName);

            DateTime birthdate = DateTime.Today.AddYears(-(randomGenerator.Next(20, 46)));

            return new MockUser(password, email, firstName, lastName, birthdate, phoneNumber);
        }

        /// <summary>
        /// Randomly generates a list of users with their associated properties.
        /// </summary>
        /// <param name="count">The number of entities to be generated</param>
        /// <returns> A randomly generated list of users.</returns>
        public List<MockUser> GenerateFakeUsers(int count)
        {
            List<MockUser> fakeUsers = new List<MockUser>();

            for (int index = 0; index < count; index++)
            {
                fakeUsers.Add(GenerateFakeUser());
            }

            return fakeUsers;
        }
    }
}
