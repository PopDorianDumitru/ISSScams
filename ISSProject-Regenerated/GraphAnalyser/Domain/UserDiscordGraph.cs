using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSfixed.ISSProject.Common.Service;
using ISSProject.Common.Logging;
using ISSProject.Common.Repository;
using ISSProject.Common.Service;
using ISSProject.Common.Wrapper;
using ISSProject_Regenerated.Common.Service;
using ISSProject_Regenerated.GraphAnalyser.Domain;

namespace ISSProject.GraphAnalyser.Domain
{
    internal class UserDiscordGraph : IUserDiscordGraph
    {
        private List<UserWrapper> users = new List<UserWrapper>();
        private readonly Dictionary<Tuple<UserWrapper, UserWrapper>, int> relations =
            new Dictionary<Tuple<UserWrapper, UserWrapper>, int>();

        public static readonly int SourceUserWeightFactor = 2;
        public static readonly int TargetUserWeightFactor = 5;
        public static readonly int MessageCountFactor = 10;

        private bool verbose = false;
        public bool Verbose
        {
            get
            {
                return verbose;
            }

            set
            {
                verbose = value;
                logger.WriteToConsole = value;
            }
        }
        private LoggingModule logger = new LoggingModule($"user_discord_graph.txt",
                                                         "User Discord Graph");

        private int FollowerCount(UserWrapper user)
        {
            return FollowerService.Provided().GetFollowers(user).Count();
        }

        private int ReceiversCount(UserWrapper user)
        {
            return MessageService.Provided().GetConversationTargets(user).Count();
        }

        private int UserWeight(UserWrapper user)
        {
            return FollowerCount(user) + ReceiversCount(user);
        }

        private int ConversationCount(UserWrapper user, UserWrapper target)
        {
            return MessageService.Provided().GetMessages(user, target).Count() +
                   MessageService.Provided().GetMessages(target, user).Count();
        }

        public int ComputeRelationScore(UserWrapper userA, UserWrapper userB)
        {
            int conversationCount = ConversationCount(userA, userB);
            if (conversationCount == 0)
            {
                return int.MaxValue;
            }

            int messageScore = MessageCountFactor * conversationCount;
            int userAWeightScore = SourceUserWeightFactor * UserWeight(userA);
            int userBWeightScore = TargetUserWeightFactor * UserWeight(userB);
            return userAWeightScore + userBWeightScore + messageScore;
        }

        public List<UserWrapper> Users
        {
            get
            {
                return users;
            }

            set
            {
                users = value;
                GenerateGraph();
            }
        }

        public void GenerateGraph()
        {
            relations.Clear();
            // O(n^2), maybe revise?
            foreach (UserWrapper userA in users)
            {
                logger.Log(LogSeverity.Info, $"Outer loop: {userA.GetId()}");
                foreach (UserWrapper userB in users)
                {
                    if (userA.GetId() == userB.GetId())
                    {
                        continue;
                    }

                    relations.Add(Tuple.Create(userA, userB),
                                   ComputeRelationScore(userA, userB));
                    logger.Log(LogSeverity.Info, $"Computed for: {userA.GetId()} -> {userB.GetId()}");
                }
            }
        }

        public UserWrapper FindMinMaxImbalance(UserWrapper userA)
        {
            UserWrapper minUser = null;
            int minScore = int.MaxValue;

            logger.Log(LogSeverity.Info, $"Computing min-max imbalance for: {userA.GetId()}");
            foreach (UserWrapper userB in users)
            {
                if (userA.GetId() == userB.GetId())
                {
                    continue;
                }

                var tuple = Tuple.Create(userA, userB);
                if (minUser == null || relations[tuple] < minScore)
                {
                    minUser = userB;
                    minScore = relations[tuple];
                }
            }

            logger.Log(LogSeverity.Info, $"Discovered min-max user: {minUser.GetId()} (score: {minScore})");
            return minUser;
        }
    }
}
