using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ClearCommand : CommandBase
    {
        public override string Name => "cls";
        public override string Description => "Clears the console";

        public override async Task Execute(CommandArguments args)
        {
            CLIGui.Instance.ConsoleLogBox.Clear();
        }
    }
}
