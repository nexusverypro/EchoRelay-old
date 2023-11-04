using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Game;
using EchoRelay.Core.Server.Storage.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ClearLockCommand : CommandBase
    {


        public override string Name => "clearlock";
        public override string Description => "Clears password locks on specified account [args: ('id' any possible) OR ('name' any possible)]";

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

            resource.ClearAccountLock();

            ConsoleLogger.LogMessage(LogType.Info, "Successfully cleared account lock on {0}!", resource.Profile.Server.DisplayName);
        }
    }
}
