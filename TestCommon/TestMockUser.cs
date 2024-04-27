using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using ISSProject.Common.Mock;

namespace TestCommon
{
    [TestClass]
    public class TestMockUser
    {
        [TestMethod]
        public void Clone_CreateAnIdenticalEntity_ShouldReturnFalseIfAnyFieldsAreNotIdentical()
        {
            MockUser user1 = new MockUser();
            MockUser user2 = new MockUser(1, "iss@yahoo.com", "Luca", "Ratan", new DateTime(2022, 4, 26, 10, 30, 0));
            MockUser clone1 = (MockUser)user1.Clone();
            MockUser clone2 = (MockUser)user2.Clone();

            Assert.IsTrue(clone1.Equals(user1) && clone2.Equals(user2));
        }
    }
}
