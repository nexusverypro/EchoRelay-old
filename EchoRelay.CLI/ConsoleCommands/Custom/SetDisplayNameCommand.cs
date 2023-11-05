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
    internal class SetDisplayName : CommandBase
    {


        public override string Name => "setdisplayname";
        public override string Description => "Sets the displayname on specified account [args: ('id' any possible) OR ('name' any possible), ('newname' any possible)]";

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

            string newname = args.GetParameter<string>("newname"); resource.Profile.SetDisplayName(newname);

            ConsoleLogger.LogMessage(LogType.Info, "Successfully set DisplayName for '{0}' to '{1}'!", resource.Profile.Server.DisplayName, newname);
        }
    }
}
