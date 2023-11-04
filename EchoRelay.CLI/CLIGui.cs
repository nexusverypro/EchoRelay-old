using EchoRelay.CLI.ConsoleCommands;
using EchoRelay.Core.CLI;
using EchoRelay.Core.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI
{
    internal class CLIGui : CoreGuiBase
    {
        public static CLIGui Instance { get; private set; }

        public override void OnCreate()
        {
            Instance = this;

            ConsoleLogger.OnMessageReceived += OnLogMessageReceived;
            m_ConsoleLogBox = new ConsoleInputBox(0, 0, (short)Console.WindowWidth, (short)(Console.WindowHeight - 1), ConsoleInputBox.Colors.LightWhite, ConsoleInputBox.Colors.Black);
            m_ConsoleInputBox = new ConsoleInputBox(0, (short)(Console.WindowHeight - 1), (short)Console.WindowWidth, 1, ConsoleInputBox.Colors.LightYellow, ConsoleInputBox.Colors.Black);

            m_ConsoleInputBox.InputPrompt = "> ";

            CommandTypes.RegisterAll();
        }

        private void OnLogMessageReceived(LogType logType, string consoleMessage)
        {
            lock (System.Console.Out)
            {
                m_ConsoleLogBox.WriteLine(consoleMessage, GetConsoleColor(logType), ConsoleInputBox.Colors.Black);
            }
        }

        private static ConsoleInputBox.Colors GetConsoleColor(LogType logType)
        {
            return logType switch
            {
                LogType.Trace => ConsoleInputBox.Colors.Gray,
                LogType.Debug => ConsoleInputBox.Colors.LightCyan,
                LogType.Info => ConsoleInputBox.Colors.LightWhite,
                LogType.Warning => ConsoleInputBox.Colors.LightYellow,
                LogType.Error => ConsoleInputBox.Colors.LightRed,
                LogType.Critical => ConsoleInputBox.Colors.DarkRed,
            };
        }

        public override void OnUpdate(float delta)
        {
            var line = m_ConsoleInputBox.ReadLine();
            Task.Run(async () => await CommandTypes.Execute(line));
        }

        public override void OnDestroy()
        {
            ConsoleLogger.OnMessageReceived -= OnLogMessageReceived;
            m_ConsoleInputBox = null;

            Instance = null;
        }

        public ConsoleInputBox ConsoleLogBox => m_ConsoleLogBox;
        public ConsoleInputBox ConsoleInputBox => m_ConsoleInputBox;

        private ConsoleInputBox m_ConsoleLogBox;
        private ConsoleInputBox m_ConsoleInputBox;
    }
}
