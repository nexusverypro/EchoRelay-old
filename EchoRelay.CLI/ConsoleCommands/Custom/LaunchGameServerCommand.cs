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
        public override string Description => "Launches a registered game server. [args: ('headless' not required possible 'true', 'false')]";

        public override async Task Execute(CommandArguments args)
        {
            GameLauncher.Launch(Constants.AppSettings.GameExecutableFilePath, GameLauncher.LaunchRole.Server, false, false, false, true, args.GetParameter("headless", false), null);
        }
    }
}
