using EchoRelay.Core.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EchoRelay.Core.Server.Messages.ServerDB.ERGameServerPlayersRejected;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class CustomKickPlayerCommand : CommandBase
    {
        public override string Name => "customkick";
        public override string Description => "Kicks a player from a game [args: ('id' any possible) OR ('name' any possible), ('error' any possible)]";

        public override async Task Execute(CommandArguments args)
        {
            string error = args.GetParameter<string>("error", "").ToLower();
            PlayerSessionError errorEnum = PlayerSessionError.Disconnected;
            if (error == "internal") { errorEnum = PlayerSessionError.Internal; } else if(error == "badrequest") { errorEnum = PlayerSessionError.BadRequest; } else if(error == "timeout") { errorEnum = PlayerSessionError.Timeout; } else if(error == "duplicate") { errorEnum = PlayerSessionError.Duplicate; } else if(error == "disconnect") { errorEnum = PlayerSessionError.Disconnected; }
            foreach (var rgsKvp in Constants.Server.ServerDBService.Registry.RegisteredGameServers)
            {
                var peer = (await rgsKvp.Value.GetPlayers())
                    .FirstOrDefault(x => x.Peer.UserId!.ToString() == args.GetParameter<string>("id", "") ||
                                         x.Peer.UserDisplayName!.ToString() == args.GetParameter<string>("name", ""));

                if (peer.Peer != null)
                {
                    await rgsKvp.Value.KickPlayerCustom(peer.PlayerSession, errorEnum);
                    ConsoleLogger.LogMessage(LogType.Warning, "Kicked '{0}' from their session", peer.Peer.UserDisplayName);
                }
            }
        }
    }
}
