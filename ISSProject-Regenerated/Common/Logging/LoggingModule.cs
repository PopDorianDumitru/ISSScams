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

    internal class LoggingModule
    {
        private static List<ConsoleColor> colors = new List<ConsoleColor>() { ConsoleColor.White, ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Red };

        private string fileName;
        private string context;
        private bool _writeToConsole = false;
        public bool WriteToConsole { get { return _writeToConsole; } set { _writeToConsole = value; } }
        public LoggingModule()
        {
            this.fileName = "logs.txt";
            context = "";
            _writeToConsole = false;
        }

        public LoggingModule(string fileName)
        {
            this.fileName = fileName;
            context = "";
            _writeToConsole = false;
        }

        public LoggingModule(string fileName, string context)
        {
            this.fileName = fileName;
            this.context = context;
            _writeToConsole = false;
        }

        public void Log(LogSeverity severity, string message)
        {
            string line;
            string level = Enum.GetName(typeof(LogSeverity), severity).ToUpper();
            line = "[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] : ";
            if (context != "")
                line += "[" + context.ToUpper() + "] ";

            line += "[" + level + "] -> " + message;

            try
            {
                StreamWriter sw = new StreamWriter(fileName, true);
                sw.WriteLine(line);
                sw.Flush();
                sw.Close();

                if (_writeToConsole)
                {
                    Console.ForegroundColor = colors[(int)severity];
                    Console.WriteLine(line);
                    Console.ResetColor();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Logging module error: " + e.Message);
            }
        }
    }
}
