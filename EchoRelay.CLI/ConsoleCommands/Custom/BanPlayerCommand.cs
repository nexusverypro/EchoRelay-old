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
        public override string Description => "Bans a player from your server. [args: ('id' any possible) OR ('name' any possible)]";

        
    }
}
