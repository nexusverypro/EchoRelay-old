using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.Core.ConsoleUtils
{
    public static class ConsoleLogger
    {
        public static event Action<LogType, string> OnMessageReceived;

        static ConsoleLogger()
        {
            var indicator = Encoding.UTF8.GetBytes("=== Log initialized at " + DateTime.Now.ToString("G") + " ===\n");

            // create the file stream for read / wrtie operations
            FileStream stream = File.Open("console.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            stream.SetLength(0);
            stream.Write(indicator, 0, indicator.Length);

            stream.Flush(); // cause the file to be reset
            stream.Close();
        }

        public static void LogMessage(LogType logType, string message, params object[] args)
        {
            // Cant log a message when it is lower than s_MinimumLogType!
            if (logType < s_MinimumLogType) return;

            bool isFormatMessage = args.Length > 0 && message.Contains("{0}");
            string consoleMessage = isFormatMessage ? string.Format(message, args) : message;

            lock (System.Console.Out)
            {
                if (!s_DisableWriteToConsole)
                {
                    ConsolePal.Write("[");
                    ConsolePal.SetTextColor(ConsoleColor.Green);
                    ConsolePal.Write(DateTime.Now.ToString("G"));
                    ConsolePal.ResetTextColor();
                    ConsolePal.Write("]    ");
                    ConsolePal.SetTextColor(GetConsoleColor(logType));
                    ConsolePal.Write(logType.ToString().PadRight("Critical    ".Length, ' '));
                    ConsolePal.WriteLine(consoleMessage);
                    ConsolePal.ResetTextColor();
                }

                if (OnMessageReceived != null) OnMessageReceived(logType, consoleMessage);
                File.AppendAllText("console.log", $"[{DateTime.Now:G}]    {logType.ToString().PadRight("Critical    ".Length, ' ')}{consoleMessage}\n");
            }
        }

        private static ConsoleColor GetConsoleColor(LogType logType)
        {
            return logType switch
            {
                LogType.Trace => ConsoleColor.DarkGray,
                LogType.Debug => ConsoleColor.Cyan,
                LogType.Info => ConsoleColor.Gray,
                LogType.Warning => ConsoleColor.Yellow,
                LogType.Error => ConsoleColor.Red,
                LogType.Critical => ConsoleColor.Red,
            };
        }

        public static readonly LogType s_MinimumLogType = LogType.Info;
        public static bool s_DisableWriteToConsole = false;
    }

    public enum LogType
    {
        Trace,
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }
}
