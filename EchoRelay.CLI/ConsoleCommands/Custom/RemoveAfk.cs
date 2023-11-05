using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Game;
using EchoRelay.Core.Server.Storage.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static EchoRelay.Core.Server.Messages.ServerDB.ERGameServerPlayersRejected;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class RemoveAFK : CommandBase
    {
        public override string Name => "removeafk";
        public override string Description => "Removes the AFK kick from a specified user [args: ('id' any possible) OR ('name' any possible)]";

        public override async Task Execute(CommandArguments args)
        {
            AccountResource? resource = null;

            if (args.HasParameter("id"))
                resource = Constants.Storage.Accounts.Get(XPlatformId.Parse(args.GetParameter<string>("id"))!);
            if (args.HasParameter("name"))
            {
                var displayName = args.GetParameter<string>("name");
                resource = Constants.Storage.Accounts.Values()
                        .FirstOrDefault(x => x.Profile.Server.DisplayName == displayName);
            }

            if (resource == null)
            {
                ConsoleLogger.LogMessage(LogType.Error, "Cannot find account.");
                return;
            }

            resource.Profile.Server.Developer!.DisableAfkTimeout = true;

            ConsoleLogger.LogMessage(LogType.Info, "Removed AFK Timeout for '{0}'", resource.Profile.Server.DisplayName);
        }
    }
}
