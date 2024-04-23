using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;
using ISSProject.Common.Wrapper;

namespace ISSProject.Common.Repository
{
    internal class MessageRepository : ICachedRepository<MessageWrapper, int>, ISizedRepository<MessageWrapper, int>
    {
        private static MessageRepository? singleton;
        public static MessageRepository Provided()
        {
            if (singleton == null)
            {
                singleton = new MessageRepository();
            }

            return singleton;
        }

        public override IEnumerable<MessageWrapper> All()
        {
            return from mockMessage in MockMessageRepository.Provided().All()
                   select new MessageWrapper(mockMessage);
        }

        public override MessageWrapper ById(int id)
        {
            if (cache.Any(id))
            {
                return cache.ById(id);
            }

            var user = new MessageWrapper(id);
            cache.Add(user);
            return user;
        }

        public override bool Delete(MessageWrapper entity)
        {
            if (cache.Any(entity.GetId()))
            {
                cache.Remove(entity);
            }

            return MockMessageRepository.Provided().Delete(entity.GetPureReference());
        }

        public override bool Insert(MessageWrapper entity)
        {
            cache.Add(entity);
            return MockMessageRepository.Provided().Insert(entity.GetPureReference());
        }

        public override int Size()
        {
            return MockMessageRepository.Provided().Size();
        }

        public override bool Update(MessageWrapper entity)
        {
            cache.Update(entity);
            return MockMessageRepository.Provided().Update(entity.GetPureReference());
        }

        public IEnumerable<MessageWrapper> GetMessages(UserWrapper sender, UserWrapper receiver)
        {
            return from messageId in MockMessageRepository.Provided()
                                     .GetMessages(sender.GetId(), receiver.GetId())
                   select new MessageWrapper(messageId);
        }

        public IEnumerable<UserWrapper> GetReceiversOfSender(UserWrapper sender)
        {
            return from userId in MockMessageRepository.Provided()
                                     .GetReceiversOfSender(sender.GetId())
                   select new UserWrapper(userId);
        }
    }
}
