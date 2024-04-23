using ISSProject.Common.Logging;
using ISSProject.Common.Repository;
using ISSProject.Common.Wrapper;
using ISSProject.GraphAnalyser.Domain;
using ISSProject.GraphAnalyser.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.GraphAnalyser.Controller
{
    internal class UserDiscordController
    {
        private UserDiscordGraph _givenUserGraph;
        public UserDiscordController(UserDiscordGraph givenUserGraph)
        {
            _givenUserGraph = givenUserGraph;
        }

        private bool _verbose = false;
        public bool Verbose { 
            get { return _verbose; }
            set { _verbose = value; logger.WriteToConsole = value; } 
        }
        private LoggingModule logger = new LoggingModule($"user_discord_controller.txt",
                                                         "User Discord Controller");
        
        public void MessageUserAfterMinMax(UserWrapper user)
        {
            var targetUser = _givenUserGraph.FindMinMaxImbalance(user);
            var score = _givenUserGraph.ComputeRelationScore(user, targetUser);

            var userName = user.GetFirstName() + " " + user.GetLastName();
            var targetName = targetUser.GetFirstName() + " " + targetUser.GetLastName();

            var messageContent = $"Please bother your friend {targetName} more, " +
                                 $"they seem to be getting bored of you...";

            MessageWrapper message = new MessageWrapper(-1, _systemUser.GetId(), user.GetId(), 
                                                        messageContent, DateTime.Now);

            MessageRepository.Provided().Insert(message);

            GraphAnalyserLog log = new GraphAnalyserLog(-1, DateTime.Now, user.GetId(), targetUser.GetId(),
                                                        score, messageContent);
            GraphAnalyserLogRepository.Provided().Insert(log);

            logger.Log(LogSeverity.Info, $"Messaged {userName} about {targetName} >> {messageContent}");
        }

        private UserWrapper _systemUser;
        public void TraverseAllUsers()
        {
            logger.Log(LogSeverity.Event, 
                "Beginning User Discord graph traversal... This might take a bit.");

            _systemUser = UserRepository.Provided().ByEmail("graph.traverser@system.ro");
            if (_systemUser == null)
            {
                logger.Log(LogSeverity.Event,
                    "No system user found. Making one for you!");
                UserWrapper _systemUser = 
                    new UserWrapper(-1, "graph.traverser@system.ro", "System", "Account", DateTime.Now);
                UserRepository.Provided().Insert(_systemUser);

                _systemUser = UserRepository.Provided().ByEmail("graph.traverser@system.ro");
            }

            foreach (var user in _givenUserGraph.Users)
                MessageUserAfterMinMax(user);

            logger.Log(LogSeverity.Event,
                "Ended User Discord graph traversal. Next one up in 1 hour...");
        }
    }
}
