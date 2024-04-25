using ISSProject.Common.DataEncryption;
using ISSProject.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace TestCommon
{
    [TestClass]
    public class TestLoggingModule
    {
        private const string TestFileName = "test_logs.txt";
        private const string TestContext = "TestContext";
        private LoggingModule loggingModule;

        [TestMethod]
        public void Log_WhenWriteToConsoleIsTrueAndConstructorWithTwoParams_ShouldWriteToFileAndConsole()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                loggingModule = new LoggingModule(TestFileName, TestContext);
                string message = "Test message";
                loggingModule.WriteToConsole = true;

                loggingModule.Log(LogSeverity.Info, message);


                Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
                //Assert.IsTrue(ConsoleOutputContains(message));
                Assert.IsTrue(sw.ToString().Contains(message));
            }
        }

        [TestMethod]
        public void Log_WhenWriteToConsoleIsFalseAndConstructorWithTwoParams_ShouldWriteToFileOnly()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                loggingModule = new LoggingModule(TestFileName, TestContext);
                string message = "Test message";
                loggingModule.WriteToConsole = false;

                loggingModule.Log(LogSeverity.Info, message);


                string fileContent = File.ReadAllText(TestFileName);
                Assert.IsTrue(fileContent.Contains(message));
                Assert.IsFalse(sw.ToString().Contains(message));
            }
        }

        [TestMethod]
        public void Log_WhenWriteToConsoleIsTrueAndConstructorWithOneParam_ShouldWriteToFileAndConsole()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                loggingModule = new LoggingModule(TestFileName);
                string message = "Test message";
                loggingModule.WriteToConsole = true;

                loggingModule.Log(LogSeverity.Info, message);


                Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
                Assert.IsTrue(sw.ToString().Contains(message));
            }
        }

        [TestMethod]
        public void Log_WhenWriteToConsoleIsFalseAndConstructorWithOneParam_ShouldWriteToFileOnly()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                loggingModule = new LoggingModule(TestFileName);
                string message = "Test message";
                loggingModule.WriteToConsole = false;

                loggingModule.Log(LogSeverity.Info, message);


                Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
                Assert.IsFalse(sw.ToString().Contains(message));
            }
        }

        [TestMethod]
        public void Log_WhenWriteToConsoleIsFalseAndConstructorWithNoParam_ShouldWriteToFileOnly()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                loggingModule = new LoggingModule();
                string message = "Test message";
                loggingModule.WriteToConsole = false;

                loggingModule.Log(LogSeverity.Info, message);


                Assert.IsTrue(File.ReadAllText("logs.txt").Contains(message));
                Assert.IsFalse(sw.ToString().Contains(message));
            }
        }

        [TestMethod]
        public void Log_WhenWriteToConsoleIsTrueAndConstructorWithNoParam_ShouldWriteToFileAndConsole()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                loggingModule = new LoggingModule();
                string message = "Test message";
                loggingModule.WriteToConsole = true;

                loggingModule.Log(LogSeverity.Info, message);


                Assert.IsTrue(File.ReadAllText("logs.txt").Contains(message));
                Assert.IsTrue(sw.ToString().Contains(message));
            }
        }
    }
}
