using ISSProject.Common.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Wrapper
{
    internal class UserWrapper : IDomainEntityWrapper<MockUser, int>, IKeyedEntity<int>
    {

        private MockUser _user;

        public UserWrapper(MockUser user)
        {
            _user = user;
        }

        public UserWrapper(int id, string email, string firstName, string lastName, DateTime birthDate)
        {
            _user = new MockUser(id, email, firstName, lastName, birthDate);
        }

        public UserWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockUser GetPureReference() { return _user; }
        public MockUser FetchUsingId(int id)
        {
            if (_user == null) _user = MockUserRepository.Provided().ById(id);
            return _user;
        }

        // Keyed Entity requirements
        public int GetId() { return _user.Id; }
        public object Clone() { return MemberwiseClone(); }

        // Anything else we need here...
        public string GetEmail() { return _user.Email; }
        public string GetFirstName() { return _user.FirstName; }
        public string GetLastName() { return _user.LastName; }

    }
}
