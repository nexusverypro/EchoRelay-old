using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.Core.ConsoleUtils
{
    /// <summary>
    /// Provides useful utils for consoles.
    /// </summary>
    public static class ConsolePal
    {
        /// <summary>
        /// Gets invoked when an error happens in the server
        /// </summary>
        public static Action<string> OnError { get; set; } = new Action<string>(err => { });

        #region Imports
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool FreeConsole();
        #endregion

        public static IntPtr CreateConsoleWindow(string title)
        {
            if (!AllocConsole())
            {
                OnError("Failed to initialize console. AllocConsole returned false.");
                return -1;
            }

            var intPtr = GetConsoleWindow();
            SetWindowText(intPtr, title);

            return intPtr;
        }

        public static void SetTextColor(ConsoleColor color) => System.Console.ForegroundColor = color;
        public static void ResetTextColor() => System.Console.ForegroundColor = ConsoleColor.Gray;

        public static void SetBackgroundColor(ConsoleColor color) => System.Console.BackgroundColor = color;
        public static void ResetBackgroundColor() => System.Console.BackgroundColor = ConsoleColor.Gray;

        public static void Write(string message) => System.Console.Out.Write(message);
        public static void Write(string format, params object[] args) => System.Console.Out.Write(string.Format(format, args));

        public static void WriteLine(string message) => System.Console.Out.WriteLine(message);
        public static void WriteLine(string format, params object[] args) => System.Console.Out.WriteLine(string.Format(format, args));

        public static bool FreeConsoleWindow() => FreeConsole();
    }
}
