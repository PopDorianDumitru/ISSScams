using ISSProject.Common.Mock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestCommon
{
    [TestClass]
    public class TestMockFollowerRepository
    {

        [TestMethod]

        public void Insert_ValidFollowerWithNonExistentId_ShouldInsert()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            MockFollower newFollower = new MockFollower(4, 5, 10);

            Assert.IsTrue(singleton.Insert(newFollower));
        }

        [TestMethod]

        public void ById_ValidFollowerWIthExistentId_ShouldReturnTheFollowerWithTheGivenId()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            MockFollower newFollower = new MockFollower(20, 11, 12);
            singleton.Insert(newFollower);

            Assert.AreEqual(singleton.ById(20).Id, 20);
            Assert.AreEqual(singleton.ById(20).UserId, 11);
            Assert.AreEqual(singleton.ById(20).FollowedUserId, 12);
        }

        [TestMethod]

        public void ById_InvalidFollowerWithNonExistentId_ShouldReturnTheFollowerWithTheGivenId()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            //Assert.AreEqual(new MockFollower(1, 2, 3), singleton.ById(1000));
            Assert.ThrowsException<KeyNotFoundException>(() => { singleton.ById(1000); });
        }

        [TestMethod]

        public void Insert_InvalidFollowerWithExistingFollower_ShouldReturnFalse()
        {
            try
            {
                MockFollowerRepository singleton;
                singleton = MockFollowerRepository.Provided();
                MockFollower newFollower = new MockFollower(2, 5, 10);
                MockFollower invalidFollower = new MockFollower(2, 1, 4);
                singleton.Insert(newFollower);
                singleton.Insert(invalidFollower);

            }
            catch (MockKeyConstraintViolation ex)
            {
                Assert.AreEqual(ex.Message, "Key Constraint Violation in Mock Database for key: 2");
            }
        }


        [TestMethod]
        public void Update_ValidFollowerWithExistingId_ShouldUpdate()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            MockFollower newFollower = new MockFollower(10, 11, 12);
            singleton.Insert(newFollower);
            MockFollower updatedFollower = new MockFollower(10, 13, 15);

            Assert.IsTrue(singleton.Update(updatedFollower));
            Assert.AreEqual(13, singleton.ById(10).UserId);
            Assert.AreEqual(15, singleton.ById(10).FollowedUserId);
        }

        [TestMethod]

        public void Update_InvalidFollowerWithNonExistentId_ShouldUpdate()
        {
            try
            {
                MockFollowerRepository singleton;
                singleton = MockFollowerRepository.Provided();
                MockFollower newFollower = new MockFollower(30, 5, 10);
                singleton.Update(newFollower);

            }
            catch (MockNoEntityViolation ex)
            {
                Assert.AreEqual(ex.Message, "No entity found in Mock Database for key: 30");
            }
        }

        [TestMethod]

        public void Delete_ValidFollowerWithExistentId_ShouldDelete()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            MockFollower newFollower = new MockFollower(2003, 11, 12);
            singleton.Insert(newFollower);

            Assert.IsTrue(singleton.Delete(newFollower));
        }

        [TestMethod]

        public void Delete_InvalidFollowerWithNonExistentId_ShouldDelete()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            MockFollower newFollower = new MockFollower(2004, 11, 12);

            Assert.IsFalse(singleton.Delete(newFollower));
        }

        [TestMethod]

        public void FollowersOf_GivenUserId_ShouldReturnAListOfIds()
        {
            MockFollowerRepository singleton;
            singleton = MockFollowerRepository.Provided();

            MockFollower newFollower1 = new MockFollower(4000, 100, 40);
            MockFollower newFollower2 = new MockFollower(4001, 101, 40);
            MockFollower newFollower3 = new MockFollower(4002, 102, 40);
            singleton.Insert(newFollower1);
            singleton.Insert(newFollower2);
            singleton.Insert(newFollower3);
            IEnumerable<int> list = singleton.FollowersOf(40);
            Assert.IsTrue(list.Contains(100));
            Assert.IsTrue(list.Contains(101));
            Assert.IsTrue(list.Contains(102));


        }
    }
}
