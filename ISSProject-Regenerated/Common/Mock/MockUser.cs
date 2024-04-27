using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Mock
{
    internal class MockUser : IKeyedEntity<int>
    {
        public int Id { get; set; }
        public string Password { get; set; }

        private string email;
        public string Email
        {
            get { return email; } set { email = value; }
        }

        private string first_name;
        public string FirstName
        {
            get { return first_name; } set { first_name = value; }
        }

        private string last_name;
        public string LastName
        {
            get { return last_name; } set { last_name = value; }
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            MockUser other = (MockUser)obj;

            // Compare string representations of the objects
            return ToString() == other.ToString();
        }
        private string phone_number;
        public string PhoneNumber
        {
            get { return phone_number; } set { phone_number = value; }
        }

        private DateTime birthdate;
        public DateTime Birthdate
        {
            get { return birthdate; } set { birthdate = value; }
        }

        public MockUser()
        {
            Id = -1;
            Password = string.Empty;
            email = string.Empty;
            first_name = string.Empty;
            last_name = string.Empty;
            birthdate = new SqlDateTime(DateTime.Now).Value;
            phone_number = string.Empty;
        }

        public MockUser(int id, string email, string firstName, string lastName, DateTime birthdate)
        {
            this.Id = id;
            Password = string.Empty;
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            this.birthdate = new SqlDateTime(birthdate).Value;
            phone_number = string.Empty;
        }

        public MockUser(int id, string password, string email, string firstName, string lastName, DateTime birthdate)
        {
            this.Id = id;
            this.Password = password;
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            this.birthdate = new SqlDateTime(birthdate).Value;
            phone_number = string.Empty;
        }

        public MockUser(int id, string password, string email, string firstName, string lastName, DateTime birthdate, string phoneNumber)
        {
            this.Id = id;
            this.Password = password;
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            this.birthdate = new SqlDateTime(birthdate).Value;
            phone_number = phoneNumber;
        }
        public MockUser(string password, string email, string firstName, string lastName, DateTime birthdate, string phoneNumber)
        {
            Id = -1;
            this.Password = password;
            this.email = email;
            first_name = firstName;
            last_name = lastName;
            this.birthdate = new SqlDateTime(birthdate).Value;
            phone_number = phoneNumber;
        }

        public override string ToString()
        {
            return "User " + first_name + " " + last_name + ", with ID " + Id + ", born on " + birthdate + ", having email address " + email + " and phone number " + phone_number;
        }

        public int GetId()
        {
            return Id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
        public bool Equals(MockUser other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id &&
                   Password == other.Password &&
                   Email == other.Email &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Birthdate == other.Birthdate &&
                   PhoneNumber == other.PhoneNumber;
        }
    }
}
