using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using ISSProject.Common.Mock;

namespace TestCommon
{
    [TestClass]
    public class TestMockUser
    {

        [TestMethod]
        public void ToString_AllConstructors_ShouldReturnTheStringRepresentationOfTheObject()
        {
            MockUser user1 = new MockUser();
            MockUser user2 = new MockUser(1, "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0));
            MockUser user3 = new MockUser(1, "lucaratan03", "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0));
            MockUser user4 = new MockUser(1, "lucaratan03", "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0), "0731427290");
            MockUser user5 = new MockUser("lucaratan03", "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0), "0731427290");

            // "User " + first_name + " " + last_name + ", with ID " + Id + ", born on " + birthdate + ", having email address " + email + " and phone number " + phone_number;

            Assert.AreEqual(user1.ToString(), "User  , with ID -1, born on " + user1.Birthdate + ", having email address  and phone number ");
            Assert.AreEqual(user2.ToString(), "User Luca Ratan, with ID 1, born on 26/04/2022 10:30:00, having email address iss@yahoo.com and phone number ");
            Assert.AreEqual(user3.ToString(), "User Luca Ratan, with ID 1, born on 26/04/2022 10:30:00, having email address iss@yahoo.com and phone number ");
            Assert.AreEqual(user4.ToString(), "User Luca Ratan, with ID 1, born on 26/04/2022 10:30:00, having email address iss@yahoo.com and phone number 0731427290");
            Assert.AreEqual(user5.ToString(), "User Luca Ratan, with ID -1, born on 26/04/2022 10:30:00, having email address iss@yahoo.com and phone number 0731427290");
        }

        [TestMethod]
        public void Clone_AllConstructors_ShouldCloneTheObject()
        {
            MockUser user1 = new MockUser();
            MockUser user2 = new MockUser(1, "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0));
            MockUser user3 = new MockUser(1, "lucaratan03", "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0));
            MockUser user4 = new MockUser(1, "lucaratan03", "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0), "0731427290");
            MockUser user5 = new MockUser("lucaratan03", "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0), "0731427290");

            MockUser clone1 = (MockUser)user1.Clone();
            MockUser clone2 = (MockUser)user2.Clone();
            MockUser clone3 = (MockUser)user3.Clone();
            MockUser clone4 = (MockUser)user4.Clone();
            MockUser clone5 = (MockUser)user5.Clone();

            Assert.AreEqual(user1.Id, clone1.Id);
            Assert.AreEqual(user1.FirstName, clone1.FirstName);
            Assert.AreEqual(user1.Password, clone1.Password);
            Assert.AreEqual(user1.LastName, clone1.LastName);
            Assert.AreEqual(user1.Birthdate, clone1.Birthdate);
            Assert.AreEqual(user1.Email, clone1.Email);
            Assert.AreEqual(user1.PhoneNumber, clone1.PhoneNumber);

            Assert.AreEqual(user2.Id, clone2.Id);
            Assert.AreEqual(user2.FirstName, clone2.FirstName);
            Assert.AreEqual(user2.Password, clone2.Password);
            Assert.AreEqual(user2.LastName, clone2.LastName);
            Assert.AreEqual(user2.Birthdate, clone2.Birthdate);
            Assert.AreEqual(user2.Email, clone2.Email);
            Assert.AreEqual(user2.PhoneNumber, clone2.PhoneNumber);

            Assert.AreEqual(user3.Id, clone3.Id);
            Assert.AreEqual(user3.FirstName, clone3.FirstName);
            Assert.AreEqual(user3.Password, clone3.Password);
            Assert.AreEqual(user3.LastName, clone3.LastName);
            Assert.AreEqual(user3.Birthdate, clone3.Birthdate);
            Assert.AreEqual(user3.Email, clone3.Email);
            Assert.AreEqual(user3.PhoneNumber, clone3.PhoneNumber);

            Assert.AreEqual(user4.Id, clone4.Id);
            Assert.AreEqual(user4.FirstName, clone4.FirstName);
            Assert.AreEqual(user4.Password, clone4.Password);
            Assert.AreEqual(user4.LastName, clone4.LastName);
            Assert.AreEqual(user4.Birthdate, clone4.Birthdate);
            Assert.AreEqual(user4.Email, clone4.Email);
            Assert.AreEqual(user4.PhoneNumber, clone4.PhoneNumber);

            Assert.AreEqual(user5.Id, clone5.Id);
            Assert.AreEqual(user5.FirstName, clone5.FirstName);
            Assert.AreEqual(user5.Password, clone5.Password);
            Assert.AreEqual(user5.LastName, clone5.LastName);
            Assert.AreEqual(user5.Birthdate, clone5.Birthdate);
            Assert.AreEqual(user5.Email, clone5.Email);
            Assert.AreEqual(user5.PhoneNumber, clone5.PhoneNumber);
        }
    }
}
