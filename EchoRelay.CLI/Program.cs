using EchoRelay.App.Settings;
using EchoRelay.Core.CLI;
using EchoRelay.Core.ConsoleUtils;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace EchoRelay.CLI
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            var appSettings = AppSettings.Load(Constants.SettingsFile);

            if (appSettings == null)
            {
                appSettings = new AppSettings("", 0, Constants.DatabaseFolder, null, true, Convert.ToBase64String(RandomNumberGenerator.GetBytes(0x20)), true, true);
                appSettings.Save(Constants.SettingsFile);
            }

            // Initialize app settings
            if (appSettings.GameExecutableFilePath == "")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    InitialDirectory = @"C:\",
                    Title = "Select Echo Arena executable",

                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "txt",
                    Filter = "Executable Files (*.exe)|*.exe",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    ReadOnlyChecked = true,
                    ShowReadOnly = true
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    appSettings.GameExecutableFilePath = openFileDialog.FileName;
                    appSettings.Save(Constants.SettingsFile);
                }
            }

            ConsoleLogger.s_DisableWriteToConsole = true;
            CoreGui.Load<CLIGui>();

            await Task.Delay(-1);
        }
    }
}