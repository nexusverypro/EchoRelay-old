using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Game;
using EchoRelay.Core.Server.Services;
using EchoRelay.Core.Server.Storage.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class UnBanPlayerCommand : CommandBase
    {
        public override string Name => "unban";
        public override string Description => "Unbans a player from your server. [args: ('id' any possible) OR ('name' any possible)]";

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

            resource.BannedUntil = null;
            Constants.Storage.Accounts.Set(resource);

            ConsoleLogger.LogMessage(LogType.Warning, "Unbanned '{0}' successfully!", resource.Profile.Server.DisplayName);
        }
    }
}
