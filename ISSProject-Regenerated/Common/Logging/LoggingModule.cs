using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace ISSProject.Common.Logging
{
    /// <summary>
    /// Possible log severity: <br/>
    /// Info <br/>
    /// Event <br/>
    /// Warning <br/>
    /// Success <br/>
    /// Error
    /// </summary>
    public enum LogSeverity
    {
        Info,
        Event,
        Warning,
        Success,
        Error
    }

    internal class LoggingModule : ILoggingModule
    {
        private static List<ConsoleColor> colors = new List<ConsoleColor>() { ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Red };

        private string fileName;
        private string context;
        private bool writeToConsole;
        public bool WriteToConsole
        {
            get { return writeToConsole; }
            set { writeToConsole = value; }
        }
        public LoggingModule()
        {
            this.fileName = "logs.txt";
            context = string.Empty;
            writeToConsole = false;
        }

        public LoggingModule(string fileName)
        {
            this.fileName = fileName;
            context = string.Empty;
            writeToConsole = false;
        }

        public LoggingModule(string fileName, string context)
        {
            this.fileName = fileName;
            this.context = context;
            writeToConsole = false;
        }

        public void Log(LogSeverity severity, string message)
        {
            string line;
            string level = Enum.GetName(typeof(LogSeverity), severity).ToUpper();
            line = "[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] : ";
            if (context != string.Empty)
            {
                line += "[" + context.ToUpper() + "] ";
            }

            line += "[" + level + "] -> " + message;

            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName, true);
                streamWriter.WriteLine(line);
                streamWriter.Flush();
                streamWriter.Close();

                if (writeToConsole)
                {
                    Console.ForegroundColor = colors[(int)severity];
                    Console.WriteLine(line);
                    Console.ResetColor();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Logging module error: " + exception.Message);
            }
        }
    }
}
