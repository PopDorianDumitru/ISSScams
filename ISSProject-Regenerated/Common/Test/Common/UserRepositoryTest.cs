using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ISSProject.Common.Mock;
using ISSProject.Common.Mock;
using ISSProject.ScamBots;

namespace ISSProject.Common.Test
{
    internal class UserRepositoryTest
    {
        public static void Test()
        {
            MockUserRepository userRepository = new MockUserRepository();
            MockUser user = new MockUser("fakepass", "an@addres.email", "Jean", "Baptiste", DateTime.Now, "0770077077");
            int initialSize = userRepository.Size();

            // get user by id that doesn't exist
            try
            {
                userRepository.ById(0);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }

            // insert user into the database
            Debug.Assert(userRepository.Insert(user));
            Debug.Assert(userRepository.Size() == initialSize + 1);

            // retrieve assigned id
            user.Id = MockUserRepository.GetUserIdByEmail(user.Email);

            // retrieve user by its assigned id
            MockUser result = userRepository.ById(user.Id);
            Debug.Assert(result.FirstName.Equals(user.FirstName));
            Debug.Assert(result.LastName.Equals(user.LastName));
            Debug.Assert(result.Id.Equals(user.Id));
            Debug.Assert(result.Password.Equals(user.Password));
            Debug.Assert(result.Email.Equals(user.Email));
            Debug.Assert(result.PhoneNumber.Equals(user.PhoneNumber));
            Debug.Assert(result.Birthdate.Equals(user.Birthdate));

            // what is this voodoo shit
            // Debug.Assert(Math.Abs((result.Birthdate - user.Birthdate).Milliseconds) < 100);

            // update user in the database
            user.FirstName = "Test";
            Debug.Assert(userRepository.Update(user));

            // retrieve user from database and check if the changes persist
            Debug.Assert(userRepository.ById(user.Id).FirstName.Equals("Test"));

            // delete user from the database
            Debug.Assert(userRepository.Delete(user));
            Debug.Assert(userRepository.Size() == initialSize);

            // check that the user is no longer in the database
            try
            {
                userRepository.ById(user.Id);
                Debug.Assert(false);
            }
            catch (Exception)
            {
            }
        }
    }
}
