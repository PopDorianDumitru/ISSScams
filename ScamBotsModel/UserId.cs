using ISSProject.MaliciousSubscriptionsBackend.Domain;

namespace MaliciousSubscriptionsDomain
{
    [TestClass]
    public class UserIDTests
    {
        [TestMethod]
        public void Equals_WhenComparingEqualUserIds_ShouldReturnTrue()
        {
            int userIdValue = 123;
            var userId1 = new UserID(userIdValue);
            var userId2 = new UserID(userIdValue);

            Assert.IsTrue(userId1.Equals(userId2));
        }

        [TestMethod]
        public void Equals_WhenComparingDifferentUserIds_ShouldReturnFalse()
        {
            var userId1 = new UserID(123);
            var userId2 = new UserID(456);

            Assert.IsFalse(userId1.Equals(userId2));
        }

        [TestMethod]
        public void GetHashCode_ForEqualUserIds_ShouldReturnSameValue()
        {
            int userIdValue = 123;
            var userId1 = new UserID(userIdValue);
            var userId2 = new UserID(userIdValue);

            int hashCode1 = userId1.GetHashCode();
            int hashCode2 = userId2.GetHashCode();

            Assert.IsTrue(hashCode1.Equals(hashCode2));
        }
    }
}
