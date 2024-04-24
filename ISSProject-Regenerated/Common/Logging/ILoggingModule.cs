namespace ISSProject.Common.Logging
{
    internal interface ILoggingModule
    {
        bool WriteToConsole { get; set; }

        void Log(LogSeverity severity, string message);
    }
}