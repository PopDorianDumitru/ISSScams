using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Mock;

namespace ISSProject_Regenerated.ScamBots.Service
{
    internal interface IFakeUserGenerator
    {
        /// <summary>
        /// Randomly generates a user with associated properties.
        /// </summary>
        /// <returns>A randomly generated user.</returns>
        MockUser GenerateFakeUser();

        /// <summary>
        /// Randomly generates a list of users with associated properties.
        /// </summary>
        /// <param name="count">The number of entities to be generated.</param>
        /// <returns>A list of randomly generated users.</returns>
        List<MockUser> GenerateFakeUsers(int count);
    }
}
