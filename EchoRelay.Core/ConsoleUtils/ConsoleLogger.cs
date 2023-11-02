using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.Core.ConsoleUtils
{
    public static class ConsoleLogger
    {
        public static void LogMessage(LogType logType, string message, params object[] args)
        {
            // Cant log a message when it is lower than s_MinimumLogType!
            if (logType < s_MinimumLogType) return;

            bool isFormatMessage = args.Length > 0 && message.Contains("{0}");
            string consoleMessage = isFormatMessage ? string.Format(message, args) : message;

            lock (System.Console.Out)
            {
                ConsolePal.Write("[");
                ConsolePal.SetTextColor(ConsoleColor.Green);
                ConsolePal.Write(DateTime.Now.ToString("G"));
                ConsolePal.ResetTextColor();
                ConsolePal.Write("] ");
                ConsolePal.SetTextColor(GetConsoleColor(logType));
                ConsolePal.Write(logType.ToString().PadRight("Critical ".Length, ' '));
                ConsolePal.WriteLine(consoleMessage);
                ConsolePal.ResetTextColor();
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
