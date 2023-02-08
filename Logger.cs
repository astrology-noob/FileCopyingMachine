namespace FileCopyingMachine
{
    internal static class Logger
    {
        public static LogType AppLogLevel = LogType.Error;
        private static string _journalName = string.Empty;

        public static void CreateLogFile(string now)
        {
            _journalName = "C:\\Users\\Мелания\\Documents\\test_logs\\log " + now + ".txt";
        }

        public static void WriteLog(string logMessage, LogType logType)
        {
            if (logType == AppLogLevel)
            {
                File.AppendAllText(_journalName, DateTime.Now.ToString());
                File.AppendAllText(_journalName, ": ");
                File.AppendAllText(_journalName, logMessage + "\n");
            }
        }

        public enum LogType
        {
            Error,
            Info,
            Debug
        }
    }
}
