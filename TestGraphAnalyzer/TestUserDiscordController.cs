
using ISSProject.Common.Wrapper;
using ISSProject.GraphAnalyser.Controller;
using ISSProject.GraphAnalyser.Domain;

namespace TestGraphAnalyzer
{
    [TestClass]
    public class TestUserDiscordController
    {
        [TestMethod]
        public void TestMessageUserAfterMinMax()
        {
            var givenUserGraph = new UserDiscordGraph(/* initialize your UserDiscordGraph here */);
            var controller = new UserDiscordController(givenUserGraph);
            //int id, string email, string firstName, string lastName, DateTime birthDate
            var user = new UserWrapper(1, "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 24, 12, 0, 0));

            controller.MessageUserAfterMinMax(user);

        }
    }
}