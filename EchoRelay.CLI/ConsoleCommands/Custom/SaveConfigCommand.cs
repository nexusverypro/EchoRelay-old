using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class SaveConfigCommand : CommandBase
    {
        public override string Name => "save_config";
        public override string Description => "Saves a config file to 'config.json' [args: ('localhost' not required 'true' or 'false')]";

        public override async Task Execute(CommandArguments args)
        {
            bool useLocalhost = args.HasParameter("local") ? args.GetParameter<bool>("local") : true;
            var host = useLocalhost ? "localhost" : Constants.Server.PublicIPAddress?.ToString();

            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "config.json"), JsonConvert.SerializeObject(Constants.Server.Settings.GenerateServiceConfig(host), Formatting.Indented));
            ConsoleLogger.LogMessage(LogType.Info, "Saved config to config.json");
        }
    }
}
