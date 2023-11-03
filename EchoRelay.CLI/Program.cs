using EchoRelay.Core.CLI;
using EchoRelay.Core.ConsoleUtils;

namespace EchoRelay.CLI
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            ConsoleLogger.s_DisableWriteToConsole = true;
            CoreGui.Load<CLIGui>();

            await Task.Delay(-1);
        }
    }
}