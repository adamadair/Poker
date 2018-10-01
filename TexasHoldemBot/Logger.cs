using System;

namespace TexasHoldemBot
{
    /// <summary>
    /// Logger - a convenience class for writing log messages.
    /// 
    /// <remarks>
    /// Riddles.io uses a convention that bot-server communication is
    /// handled on standard input/standard output, but anything written
    /// to standard error is sent to a log file that can be viewed
    /// after a session.
    /// </remarks>
    /// </summary>
    static class Logger
    {
        public static void Info(string message)
        {
            Log($"Info: {message}");
        }

        public static void Error(string message)
        {
            Log($"Error: {message}");
        }

        public static void Error(string message, Exception ex)
        {
            Log($"Error: {message}\nException: {ex}\n{ex.StackTrace}");
        }

        private static void Log(string message)
        {
            BotIo.Log.WriteLine(message);
        }
    }
}
