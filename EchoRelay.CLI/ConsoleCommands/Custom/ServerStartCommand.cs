using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Server.Storage.Filesystem;
using EchoRelay.Core.Server.Storage;
using EchoRelay.Core.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EchoRelay.App.Settings;
using System.Security.Cryptography;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ServerStartCommand : CommandBase
    {
        public override string Name => "server_start";
        public override string Description => "Starts all Echo services. [args: ('port' required possible 0 - 65535), ('apiKey' not required any possible)]";

        public override async Task Execute(CommandArguments args)
        {
            if (!args.HasParameter("port"))
            {
                ConsoleLogger.LogMessage(LogType.Error, "Cannot initialize server without a port! Use the -port argument.");
                return;
            }

            if (Constants.Server != null && Constants.Server.Running)
            {
                ConsoleLogger.LogMessage(LogType.Error, "Cannot initialize server while its running!");
                return;
            }

            // Create our file system storage and open it.
            Constants.Storage = new FilesystemServerStorage(Constants.DatabaseFolder);
            Constants.Storage.Open();

            var apiKey = args.HasParameter("apiKey") ? args.GetParameter<string>("apiKey") : Constants.AppSettings.ServerDBApiKey;
            // Create a server instance and set up our event handlers
            Constants.Server = new Server(Constants.Storage,
                new ServerSettings(
                    port: args.GetParameter<ushort>("port"),
                    serverDbApiKey: apiKey,
                    favorPopulationOverPing: Constants.AppSettings.MatchingPopulationOverPing,
                    forceIntoAnySessionIfCreationFails: Constants.AppSettings.MatchingForceIntoAnySessionOnFailure
                    )
                );
            ConsoleLogger.LogMessage(LogType.Info, "Using DB API key: '{0}'", apiKey);

            await Constants.Server.Start();
        }
    }
}
