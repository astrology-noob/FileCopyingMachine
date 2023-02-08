namespace FileCopyingMachine
{
    internal static class ExceptionHandler
    {
        public static void HandleException(string exceptionMessage)
        {
            Console.WriteLine(exceptionMessage);
            Logger.WriteLog(exceptionMessage, Logger.LogType.Error);
            Environment.Exit(1);
        }
    }
}
