using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using ISSProject.Common.Logging;
using ISSProject.Common.Mock;
using ISSProject.Common.Mock;
using ISSProject.Common.Test;
using ISSProject.Common.Test.Common;
using ISSProject.Common.Test.ScamBots;

namespace ISSProject.Common.Test
{
    internal class MainTester
    {
        public static void Test()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("///         --- MODULE TESTING ---          ///\n");
            Console.ResetColor();

            Console.WriteLine("Running encryption/decryption module test...");
            EncrypterTest.Test();
            Console.WriteLine("Finished encryption/decryption module test!");

            Console.WriteLine();

            Console.WriteLine("Running logging module test...");
            LoggingModuleTest.Test();
            Console.WriteLine("Finished logging module test!");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("///        --- REPOSITORY TESTING ---       ///\n");
            Console.ResetColor();

            Console.WriteLine("Running user repository test...");
            UserRepositoryTest.Test();
            Console.WriteLine("Finished user repository test!");

            Console.WriteLine();

            Console.WriteLine("Running fake user repository test...");
            FakeUserRepositoryTest.Test();
            Console.WriteLine("Finished fake user repository test!");

            Console.WriteLine();

            Console.WriteLine("Running message repository test...");
            MessageRepositoryTest.Test();
            Console.WriteLine("Finished message repository test!");

            Console.WriteLine();

            Console.WriteLine("Running scam message template repository test...");
            ScamMessageRepositoryTest.Test();
            Console.WriteLine("Finished scam message template repository test!");

            Console.WriteLine();

            Console.WriteLine("Running scam message link repository test...");
            ScamMessageLinkRepositoryTest.Test();
            Console.WriteLine("Finished scam message link repository test!");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("///         --- SERVICE TESTING ---         ///");
            Console.ResetColor();

            Console.WriteLine();

            Console.WriteLine("Running scam message generator test...");
            ScamMessageGeneratorTest.Test();
            Console.WriteLine("Finished scam message generator test!");

            Console.WriteLine();

            Console.WriteLine("Running fake user generator test...");
            FakeUserGeneratorTest.Test();
            Console.WriteLine("Finished fake user generator test!");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("///-----------------------------------------///");
            Console.WriteLine("///--------------TESTS FINISHED-------------///");
            Console.WriteLine("///-----------------------------------------///");
            Console.ResetColor();
        }
    }
}
