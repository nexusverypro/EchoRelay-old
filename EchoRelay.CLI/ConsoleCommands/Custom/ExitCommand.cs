using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ExitCommand : CommandBase
    {
        public override string Name => "exit";
        public override string Description => "Closes the server and the CLI.";

        public override async Task Execute(CommandArguments args)
        {
            await CommandTypes.Execute("server_stop");
            Environment.Exit(0);
        }
    }
}
