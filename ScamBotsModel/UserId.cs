using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace MaliciousSubscriptionsDomain
{
    [TestClass]
    public class UserIDTests
    {
        [TestMethod]
        public void Constructor_ShouldSetUserIDCorrectly()
        {
            int expectedUserId = 123;

            var userId = new UserID(expectedUserId);

            Assert.AreEqual(expectedUserId, userId.GetId());
        }

        [TestMethod]
        public void Equals_ShouldReturnTrue_WhenComparingEqualUserIds()
        {
            int userIdValue = 123;
            var userId1 = new UserID(userIdValue);
            var userId2 = new UserID(userIdValue);

            bool areEqual = userId1.Equals(userId2);

            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Equals_ShouldReturnFalse_WhenComparingDifferentUserIds()
        {
            var userId1 = new UserID(123);
            var userId2 = new UserID(456);

            bool areEqual = userId1.Equals(userId2);

            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GetHashCode_ShouldReturnSameValue_ForEqualUserIds()
        {
            int userIdValue = 123;
            var userId1 = new UserID(userIdValue);
            var userId2 = new UserID(userIdValue);

            int hashCode1 = userId1.GetHashCode();
            int hashCode2 = userId2.GetHashCode();

            Assert.AreEqual(hashCode1, hashCode2);
        }
    }
}
