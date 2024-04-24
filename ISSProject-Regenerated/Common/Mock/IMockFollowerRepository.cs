using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common;
using ISSProject.Common.Mock;

namespace ISSProject_Regenerated.Common.Mock
{
    internal interface IMockFollowerRepository : IRepository<MockFollower, int>
    {
        /// <summary>
        /// Retrieves all mock followers.
        /// </summary>
        /// <returns>An enumerable collection of mock followers.</returns>
        IEnumerable<MockFollower> All();

        /// <summary>
        /// Retrieves a mock follower by its ID.
        /// </summary>
        /// <param name="id">The ID of the mock follower to retrieve.</param>
        /// <returns>The mock follower if found.</returns>
        MockFollower ById(int id);

        /// <summary>
        /// Deletes a mock follower from the repository.
        /// </summary>
        /// <param name="entity">The mock follower to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        bool Delete(MockFollower entity);

        /// <summary>
        /// Inserts a mock follower into the repository.
        /// </summary>
        /// <param name="entity">The mock follower to insert.</param>
        /// <returns>True if the insertion was successful; otherwise, false.</returns>
        bool Insert(MockFollower entity);

        /// <summary>
        /// Updates a mock follower in the repository.
        /// </summary>
        /// <param name="entity">The mock follower to update.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        bool Update(MockFollower entity);

        /// <summary>
        /// Retrieves the IDs of users who are followers of a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve followers for.</param>
        /// <returns>An enumerable collection of follower user IDs.</returns>
        IEnumerable<int> FollowersOf(int userId);
    }
}
