using EchoRelay.Core.ConsoleUtils;

namespace EchoRelay
{
    internal static class Program
    {
        static IntPtr s_ConsolePtr;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Setup core events
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                ConsoleLogger.LogMessage(LogType.Critical, "Unhandled exception: {0}", e.ExceptionObject);
            };

            s_ConsolePtr = ConsolePal.CreateConsoleWindow("EchoRelay Console [DEBUG]");
            ConsoleLogger.LogMessage(LogType.Info, "Initialized ConsolePal window. IntPtr = {0}", s_ConsolePtr);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainWindow());
        }
    }
}
