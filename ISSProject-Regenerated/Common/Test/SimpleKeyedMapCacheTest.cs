using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISSProject.Common.Cache;
using ISSProject_Regenerated.Common.Test;

namespace ISSProject.Common.Test
{
    internal class SimpleKeyedMapCacheTest : ITest
    {
        public static void Test(string[] args)
        {
            var cache = new SimpleKeyedMapCache<ExampleEntity, int>();

            var example1 = new ExampleEntity(1, "Hello");
            var example2 = new ExampleEntity(2, "World");

            cache.Add(example1);

            if (cache.Any(example1.GetId()))
            {
                Console.WriteLine("Yes, example 1");
            }

            if (cache.Any(example2.GetId()))
            {
                Console.WriteLine("No, example 2");
            }

            example1.Name = "Test";

            Console.WriteLine(cache.ById(1).Name);

            cache.Update(example1);

            Console.WriteLine(cache.ById(1).Name);

            Console.Read();
        }
    }
}
