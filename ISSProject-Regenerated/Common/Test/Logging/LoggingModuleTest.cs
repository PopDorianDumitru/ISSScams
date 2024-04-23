using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ISSProject.Common.Logging;
using ISSProject_Regenerated.Common.Test;

namespace ISSProject.Common.Test
{
    internal class LoggingModuleTest : ITest
    {
        public static void Test()
        {
            LoggingModule logger = new LoggingModule("log_test.txt");
            logger.WriteToConsole = true;

            string basePattern = @"\A\[[0-9]{2}/[01][0-9]/[0-9]{4} [0-2][0-9]:[0-5][0-9]:[0-5][0-9]] : (\[[A-Z]*] )?\[";
            List<string> logOutputs = new List<string>()
            {
                @"INFO] -> This is an information log; its colour should be white.\Z",
                @"EVENT] -> This is an event log; its colour should be cyan.\Z",
                @"WARNING] -> This is a warning log; its colour should be yellow.\Z",
                @"SUCCESS] -> This is a success log; its colour should be green.\Z",
                @"ERROR] -> This is an error log; its colour should be red.\Z"
            };

            string line;
            int currentLine = 0;
            try
            {
                StreamWriter writer = new StreamWriter("log_test.txt", false);
                writer.Write(string.Empty);
                writer.Flush();
                writer.Close();

                logger.Log(LogSeverity.Info, "This is an information log; its colour should be white.");
                logger.Log(LogSeverity.Event, "This is an event log; its colour should be cyan.");
                logger.Log(LogSeverity.Warning, "This is a warning log; its colour should be yellow.");
                logger.Log(LogSeverity.Success, "This is a success log; its colour should be green.");
                logger.Log(LogSeverity.Error, "This is an error log; its colour should be red.");

                StreamReader reader = new StreamReader("log_test.txt");
                line = reader.ReadLine();

                while (line != null && currentLine < 5)
                {
                    Regex logPattern = new Regex(basePattern + logOutputs[currentLine]);
                    Debug.Assert(logPattern.IsMatch(line));

                    line = reader.ReadLine();
                    currentLine++;
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}
