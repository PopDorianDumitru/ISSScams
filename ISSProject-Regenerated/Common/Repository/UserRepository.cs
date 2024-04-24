using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Repository
{
    internal class UserRepository : ICachedRepository<UserWrapper, int>,  IUserRepository
    {
        private static UserRepository? singleton;
        public static UserRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new UserRepository();
            }

            return singleton;
        }

        public override IEnumerable<UserWrapper> All()
        {
            return from mockUser in MockUserRepository.Provided().All()
                   select new UserWrapper(mockUser);
        }

        public override UserWrapper ById(int id)
        {
            if (cache.Any(id))
            {
                return cache.ById(id);
            }

            var user = new UserWrapper(id);
            cache.Add(user);
            return user;
        }

        public bool Any(int id)
        {
            if (cache.Any(id))
            {
                return true;
            }

            try
            {
                new UserWrapper(id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserWrapper ByEmail(string email)
        {
            int id = MockUserRepository.GetUserIdByEmail(email);
            if (id == -1)
            {
                return null;
            }

            if (cache.Any(id))
            {
                return cache.ById(id);
            }

            var user = new UserWrapper(id);
            cache.Add(user);
            return user;
        }

        public override bool Delete(UserWrapper entity)
        {
            if (cache.Any(entity.GetId()))
            {
                cache.Remove(entity);
            }

            return MockUserRepository.Provided().Delete(entity.GetPureReference());
        }

        public override bool Insert(UserWrapper entity)
        {
            cache.Add(entity);
            return MockUserRepository.Provided().Insert(entity.GetPureReference());
        }

        public override int Size()
        {
            return MockUserRepository.Provided().Size();
        }

        public override bool Update(UserWrapper entity)
        {
            cache.Update(entity);
            return MockUserRepository.Provided().Update(entity.GetPureReference());
        }
    }
}
