using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ISSProject.Common.DataEncryption;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;
using ISSProject.ScamBots.Model;
using ISSProject.ScamBots.Service;

namespace ISSProject.Common.Test.ScamBots
{
    internal class FakeUserGeneratorTest
    {
        public static void Test()
        {
            ShiftEncrypter encrypter1 = new ShiftEncrypter(5);
            ShiftEncrypter encrypter2 = new ShiftEncrypter(15);

            FakeUserGenerator generator = new FakeUserGenerator();
            FakeUserRepository repository = new FakeUserRepository();

            Regex phoneNumberPattern = new Regex(@"\A22[0-9]{8}\Z");
            Regex emailPattern = new Regex(@"\A[a-zA-Z]+_[a-zA-Z]+_[0-9]{5,6}_22[0-9]{8}@gmail\.com\Z");
            Regex passwordPattern = new Regex(@"[A-Za-z]+_[A-Za-z]+");

            // test generation of one user
            MockUser fakeUser = generator.GenerateFakeUser();
            Debug.Assert(repository.Insert(fakeUser));
            fakeUser.Id = repository.UserIdByEmail(fakeUser.Email);

            Debug.Assert(phoneNumberPattern.IsMatch(fakeUser.PhoneNumber));
            Debug.Assert(emailPattern.IsMatch(fakeUser.Email));

            Debug.Assert(passwordPattern.IsMatch(fakeUser.Password));
            Debug.Assert(fakeUser.Password.Equals(encrypter1.EncryptString(fakeUser.FirstName) + "_" + encrypter2.EncryptString(fakeUser.LastName)));
            string[] pass = fakeUser.Password.Split('_');
            Debug.Assert(pass.Length == 2);
            Debug.Assert(fakeUser.FirstName.Equals(encrypter1.DecryptString(pass[0])) && fakeUser.LastName.Equals(encrypter2.DecryptString(pass[1])));

            Debug.Assert(repository.Delete(fakeUser));

            // test generation of multiple users
            List<MockUser> generatedUsers = generator.GenerateFakeUsers(100);

            foreach (MockUser generatedUser in generatedUsers)
            {
                try
                {
                    repository.ById(generatedUser.Id);
                }
                catch (Exception)
                {
                    Debug.Assert(repository.Insert(generatedUser));
                    generatedUser.Id = repository.UserIdByEmail(generatedUser.Email);

                    Debug.Assert(phoneNumberPattern.IsMatch(generatedUser.PhoneNumber));
                    Debug.Assert(emailPattern.IsMatch(generatedUser.Email));

                    Debug.Assert(passwordPattern.IsMatch(generatedUser.Password));
                    Debug.Assert(generatedUser.Password.Equals(encrypter1.EncryptString(generatedUser.FirstName) + "_" + encrypter2.EncryptString(generatedUser.LastName)));
                    pass = generatedUser.Password.Split('_');
                    Debug.Assert(pass.Length == 2);
                    Debug.Assert(generatedUser.FirstName.Equals(encrypter1.DecryptString(pass[0])) && generatedUser.LastName.Equals(encrypter2.DecryptString(pass[1])));

                    Debug.Assert(repository.Delete(generatedUser));
                }
            }
        }
    }
}
