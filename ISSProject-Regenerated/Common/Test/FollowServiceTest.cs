using ISSProject.Common.Mock;
using ISSProject.Common.Service;
using ISSProject.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISSProject.Common.Test
{
    internal class FollowServiceTest
    {
        public static void Test(String[] args)
        {
            MockFollowerRepository.ResetMockDatabase();

            FollowerService followService = new FollowerService();

            foreach (var item in MockFollowerRepository.Provided().All())
                Console.WriteLine($"Entry {item.Id}: User{item.UserId} Follow{item.FollowedUserId}");

            UserWrapper userId1 = new UserWrapper(1);

            if (followService.GetFollowers(userId1).Count() ==
                MockFollowerRepository.Provided().FollowersOf(1).Count())
                Console.WriteLine("Wrapping seems alright!");
            else
            {
                Console.WriteLine("Something is wrong with wrapping.");
                Console.WriteLine($"Wrapped Service counted {followService.GetFollowers(userId1).Count()} entires.");
                Console.WriteLine("Actual results of wrapped repository are:");
                foreach (var item in MockFollowerRepository.Provided().FollowersOf(1))
                    Console.Write($"-- ID {item} ");
            }

            Console.ReadLine();

        }

    }
}
