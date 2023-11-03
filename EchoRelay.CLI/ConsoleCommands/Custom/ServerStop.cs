using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ServerStop : CommandBase
    {
        public override string Name => "server_stop";
        public override string Description => "Stops all Echo services.";

        public override async Task Execute(CommandArguments args)
        {
            if (Constants.Server != null && Constants.Server.Running)
                Constants.Server.Stop();
        }
    }
}
