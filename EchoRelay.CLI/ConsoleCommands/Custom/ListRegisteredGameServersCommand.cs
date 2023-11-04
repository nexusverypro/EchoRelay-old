using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Server.Services.ServerDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ListRegisteredGameServersCommand : CommandBase
    {
        public override string Name => "all_rgs";
        public override string Description => "Lists all registered game servers.";

        public override async Task Execute(CommandArguments args)
        {
            ConsoleLogger.LogMessage(LogType.Info, "GS count: {0}", Constants.Server.ServerDBService.Registry.RegisteredGameServers.Count);
            foreach (var rgs in Constants.Server.ServerDBService.Registry.RegisteredGameServers.Values)
            {
                ConsoleLogger.LogMessage(LogType.Info, "Registered game server found: {0}", rgs.ToString());
            }
        }
    }
}
