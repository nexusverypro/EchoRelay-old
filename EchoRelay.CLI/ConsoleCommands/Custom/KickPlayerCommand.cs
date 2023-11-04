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
        public override string Description => "Kicks a player from a game. [args: ('id' any possible) OR ('name' any possible)]";

        public override async Task Execute(CommandArguments args)
        {
            foreach (var rgsKvp in Constants.Server.ServerDBService.Registry.RegisteredGameServers)
            {
                var peer = (await rgsKvp.Value.GetPlayers())
                    .FirstOrDefault(x => x.Peer.UserId!.ToString() == args.GetParameter<string>("id", "") ||
                                         x.Peer.UserDisplayName!.ToString() == args.GetParameter<string>("name", ""));

                if (peer.Peer != null)
                {
                    await rgsKvp.Value.KickPlayer(peer.PlayerSession);
                    ConsoleLogger.LogMessage(LogType.Warning, "Kicked '{0}' from their session", peer.Peer.UserDisplayName);
                }
            }
        }
    }
}
