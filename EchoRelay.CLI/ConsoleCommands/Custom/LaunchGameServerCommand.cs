using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class LaunchGameServerCommand : CommandBase
    {
        public override string Name => "launch_rgs";
        public override string Description => "Launches a registered game server. [args: ('count' not required possible '1 - 10'), ('headless' not required possible 'true', 'false')]";

        public override async Task Execute(CommandArguments args)
        {
            int count = Math.Clamp(args.GetParameter<int>("count", 1), 1, 10);

            for (int i = 0; i < count; i++)
                GameLauncher.Launch(Constants.AppSettings.GameExecutableFilePath, GameLauncher.LaunchRole.Server, false, false, false, true, args.GetParameter("headless", false), null);
            ConsoleLogger.LogMessage(LogType.Warning, "Started {0} game servers.", count);
        }
    }
}
