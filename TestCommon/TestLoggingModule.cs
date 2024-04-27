using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ISSProject.Common.Logging;

namespace TestCommon
{
    [TestClass]
    public class LoggingModuleTests
    {
        private const string TestFileName = "test_logs.txt";
        private const string TestContext = "TestContext";
        private LoggingModule? loggingModule;

        [TestMethod]
        public void Log_WriteToConsoleIsTrue_FileAndConsoleContainsMessage()
        {
            var mockConsole = new Mock<TextWriter>();
            Console.SetOut(mockConsole.Object);
            loggingModule = new LoggingModule(TestFileName, TestContext);
            string message = "Test message";
            loggingModule.WriteToConsole = true;
            loggingModule.Log(LogSeverity.Info, message);
            Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
        }

        [TestMethod]
        public void Log_WriteToConsoleIsFalse_FileContainsMessage()
        {
            loggingModule = new LoggingModule(TestFileName, TestContext);
            string message = "Test message";
            loggingModule.WriteToConsole = false;
            loggingModule.Log(LogSeverity.Info, message);
            Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
        }

        [TestMethod]
        public void Log_WriteToConsoleIsTrue_FileAndConsoleContainsMessage_NoContext()
        {
            var mockConsole = new Mock<TextWriter>();
            Console.SetOut(mockConsole.Object);
            loggingModule = new LoggingModule(TestFileName);
            string message = "Test message";
            loggingModule.WriteToConsole = true;
            loggingModule.Log(LogSeverity.Info, message);
            Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
        }

        [TestMethod]
        public void Log_WriteToConsoleIsFalse_FileContainsMessage_NoContext()
        {
            loggingModule = new LoggingModule(TestFileName);
            string message = "Test message";
            loggingModule.WriteToConsole = false;
            loggingModule.Log(LogSeverity.Info, message);
            Assert.IsTrue(File.ReadAllText(TestFileName).Contains(message));
        }

        [TestMethod]
        public void Log_WriteToConsoleIsFalse_FileContainsMessage_NoParams()
        {
            loggingModule = new LoggingModule();
            string message = "Test message";
            loggingModule.WriteToConsole = false;
            loggingModule.Log(LogSeverity.Info, message);
            Assert.IsTrue(File.ReadAllText("logs.txt").Contains(message));
        }

        [TestMethod]
        public void Log_WriteToConsoleIsTrue_FileAndConsoleContainsMessage_NoParams()
        {
            var mockConsole = new Mock<TextWriter>();
            Console.SetOut(mockConsole.Object);
            loggingModule = new LoggingModule();
            string message = "Test message";
            loggingModule.WriteToConsole = true;
            loggingModule.Log(LogSeverity.Info, message);
            Assert.IsTrue(File.ReadAllText("logs.txt").Contains(message));
        }
    }
}
