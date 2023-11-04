using EchoRelay.Core.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class KickPlayerCommand : CommandBase
    {
        public override string Name => "kick";
        public override string Description => "Kicks a player from a game. [args: ('id' any possible)]";

        public override async Task Execute(CommandArguments args)
        {
            if (!args.HasParameter("id"))
            {
                ConsoleLogger.LogMessage(LogType.Critical, "Cannot kick a player without their ID.");
                return;
            }

            foreach (var rgsKvp in Constants.Server.ServerDBService.Registry.RegisteredGameServers)
            {
                var peer = (await rgsKvp.Value.GetPlayers())
                    .FirstOrDefault(x => x.Peer.UserId!.ToString() == args.GetParameter<string>("id"));

                await rgsKvp.Value.KickPlayer(peer.PlayerSession);
            }
        }
    }
}
