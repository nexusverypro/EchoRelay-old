using EchoRelay.Core.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands.Custom
{
    internal class ListAllAccountsCommand : CommandBase
    {
        public override string Name => "all_accounts";
        public override string Description => "Lists all accounts in the Account database.";

        public override async Task Execute(CommandArguments args)
        {
            foreach (var item in Constants.Storage.Accounts.Values()) 
            {
                ConsoleLogger.LogMessage(LogType.Info, "Account found: {0}", item.ToString());
            }
        }
    }
}
