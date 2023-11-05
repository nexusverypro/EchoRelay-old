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
    internal class ChooseExecutableCommand : CommandBase
    {
        public override string Name => "setexec";
        public override string Description => "Set a new executable as the Echo VR executable, no args needed.";

        public override async Task Execute(CommandArguments args)
        {
            ConsoleLogger.LogMessage(LogType.Info, "Test Command successfully executed!");
            await Program.SetNewExecutable();
        }
    }
}
