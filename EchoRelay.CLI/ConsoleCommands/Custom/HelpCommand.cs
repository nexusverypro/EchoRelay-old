using EchoRelay.Core.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class HelpCommand : CommandBase
    {
        public override string Name => "help";
        public override string Description => "Gives a brief overview of all commands.";

        public override async Task Execute(CommandArguments args)
        {
            var commands = new List<CommandBase>(CommandTypes.Commands);
            commands.RemoveAll(x => x.Name == "help");

            foreach (var command in commands)
            {
                ConsoleLogger.LogMessage(LogType.Info, "{0}: {1}", command.Name, command.Description);
            }
        }
    }
}
