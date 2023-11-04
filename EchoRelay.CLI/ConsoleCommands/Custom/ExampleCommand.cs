using EchoRelay.Core.ConsoleUtils;
using EchoRelay.Core.Server.Storage.Filesystem;
using EchoRelay.Core.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ExampleCommand : CommandBase
    {
        public override string Name => "ExampleCommand";
        public override string Description => "An example command";

        public override async Task Execute(CommandArguments args)
        {
            ConsoleLogger.LogMessage(LogType.Info, "Test Command successfully executed!");
        }
    }
}
