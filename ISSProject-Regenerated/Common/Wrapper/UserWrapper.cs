using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace ISSProject.Common.Wrapper
{
    internal class UserWrapper : IDomainEntityWrapper<MockUser, int>, IKeyedEntity<int>
    {
        private MockUser user;

        public UserWrapper(MockUser user)
        {
            this.user = user;
        }

        public UserWrapper(int id, string email, string firstName, string lastName, DateTime birthDate)
        {
            user = new MockUser(id, email, firstName, lastName, birthDate);
        }

        public UserWrapper(int id)
        {
            FetchUsingId(id);
        }

        public MockUser GetPureReference()
        {
            return user;
        }
        public MockUser FetchUsingId(int id)
        {
            if (user == null)
            {
                user = MockUserRepository.Provided().ById(id);
            }

            return user;
        }

        // Keyed Entity requirements
        public int GetId()
        {
            return user.Id;
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        // Anything else we need here...
        public string GetEmail()
        {
            return user.Email;
        }
        public string GetFirstName()
        {
            return user.FirstName;
        }
        public string GetLastName()
        {
            return user.LastName;
        }
    }
}
