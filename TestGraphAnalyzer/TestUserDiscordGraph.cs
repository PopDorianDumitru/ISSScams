using ISSProject.Common.Repository;
using ISSProject.Common.Service;
using ISSProject.Common.Wrapper;
using ISSProject.GraphAnalyser.Domain;
using ISSProject_Regenerated.Common.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGraphAnalyzer
{
    [TestClass]
    public class TestUserDiscordGraph
    {
        private UserDiscordGraph _graph;

        [TestInitialize]
        public void TestInitialize()
        {
            _graph = new UserDiscordGraph();
            List<UserWrapper> users = new List<UserWrapper>();
            users.Add(new UserWrapper(1, "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 24, 12, 0, 0)));
            users.Add(new UserWrapper(2, "iss2@yahoo.com", "Denis", "Pop", new DateTime(2023, 4, 24, 12, 0, 0)));
            users.Add(new UserWrapper(3, "iss3@yahoo.com", "Dorian", "Pop", new DateTime(2024, 4, 24, 12, 0, 0)));
            users.Add(new UserWrapper(4, "iss4@yahoo.com", "Crispy", "Popan", new DateTime(2020, 4, 24, 12, 0, 0)));
            _graph.Users = users;


        }

        [TestMethod]
        public void ComputeRelationScore_ValidUsers_CountsScore()
        {
            Assert.AreEqual(0, _graph.ComputeRelationScore(userA: _graph.Users[0], userB: _graph.Users[1]));
        }
    }
}
