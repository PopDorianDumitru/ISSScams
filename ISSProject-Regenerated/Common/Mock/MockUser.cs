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

        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _password;
        public string Password { get { return _password; } set { _password = value; } }

        private string _email;
        public string Email { get { return _email; } set { _email = value; } }


        private string _first_name;
        public string FirstName { get { return _first_name; } set { _first_name = value; } }


        private string _last_name;
        public string LastName { get { return _last_name; } set { _last_name = value; } }

        private string _phone_number;
        public string PhoneNumber { get { return _phone_number; } set { _phone_number = value; } }


        private DateTime _birthdate;
        public DateTime Birthdate { get { return _birthdate; } set { _birthdate = value; } }

        public MockUser()
        {
            _id = -1;
            _password = "";
            _email = "";
            _first_name = "";
            _last_name = "";
            _birthdate = new SqlDateTime(DateTime.Now).Value;
            _phone_number = "";
        }

        public MockUser(int id, string email, string firstName, string lastName, DateTime birthdate)
        {
            _id = id;
            _password = "";
            _email = email;
            _first_name = firstName;
            _last_name = lastName;
            _birthdate = new SqlDateTime(birthdate).Value;
            _phone_number = "";
        }

        public MockUser(int id, string password, string email, string firstName, string lastName, DateTime birthdate) {
            _id = id;
            _password = password;
            _email = email;
            _first_name = firstName;
            _last_name = lastName; 
            _birthdate = new SqlDateTime(birthdate).Value;
            _phone_number = "";
        }

        public MockUser(int id, string password, string email, string firstName, string lastName, DateTime birthdate, string phoneNumber)
        {
            _id = id;
            _password = password;
            _email = email;
            _first_name = firstName;
            _last_name = lastName;
            _birthdate = new SqlDateTime(birthdate).Value;
            _phone_number = phoneNumber;
        }
        public MockUser(string password, string email, string firstName, string lastName, DateTime birthdate, string phoneNumber)
        {
            _id = -1;
            _password = password;
            _email = email;
            _first_name = firstName;
            _last_name = lastName;
            _birthdate = new SqlDateTime(birthdate).Value;
            _phone_number = phoneNumber;
        }

        public override string ToString()
        {
            return "User " + _first_name + " " + _last_name + ", with ID " + _id + ", born on " + _birthdate + ", having email address " + _email + " and phone number " + _phone_number;
        }

        public int GetId()
        {
            return _id;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
