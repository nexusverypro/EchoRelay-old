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
    internal class BanPlayerCommand : CommandBase
    {
        public override string Name => "ban";
        public override string Description => "Bans a player from your server. [args: ('id' any possible) OR ('name' any possible), ('time' any possible with 'h, d, m' suffix)]";

        public TimeSpan GetFromTimeFrameString(string timeFrameString)
        {
            var period = int.Parse(timeFrameString.Remove(timeFrameString.Length - 1, 1));
            var timeType = timeFrameString.Substring(timeFrameString.Length - 1, 1);

            return timeType switch
            {
                "h" => TimeSpan.FromHours(period),
                "d" => TimeSpan.FromDays(period),
                "m" => TimeSpan.FromDays(period * 30),
                _ => throw new Exception("No possible time frame given! Possible time frames = h (hours), d (days), m (months)")
            };
        }

        public override async Task Execute(CommandArguments args)
        {
            AccountResource? resource = null;

            var timeFrame = GetFromTimeFrameString(args.GetParameter<string>("time"));
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

            resource.BannedUntil = DateTime.UtcNow + timeFrame;
            Constants.Storage.Accounts.Set(resource);

            // mathie can take it from here
            // all you have to do is use Constants.Storage.Accounts
            // to save the account resouce
        }
    }
}
