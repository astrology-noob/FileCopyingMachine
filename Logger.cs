using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopyingMachine
{
    internal class Logger
    {
        public string? appLogLevel;
        public string? journalName;

        public void CreateLogFile(string now)
        {
            journalName = "C:\\Users\\Мелания\\Documents\\test_logs\\log " + now + ".txt";
        }

        public void WriteLog(string logMessage, LogType logType)
        {
            if (logType.ToString() == appLogLevel)
            {
                File.AppendAllText(journalName, DateTime.UtcNow.ToString());
                File.AppendAllText(journalName, ": ");
                File.AppendAllText(journalName, logMessage + "\n");
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
