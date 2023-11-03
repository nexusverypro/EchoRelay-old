using EchoRelay.Core.ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands
{
    internal static class CommandTypes
    {
        public static void RegisterAll()
        {
            var assembly = Assembly.GetAssembly(typeof(CommandTypes));
            var commandTypes = (from x in assembly.GetTypes()
                                where x.BaseType != null && x.BaseType.GUID == typeof(CommandBase).GUID
                                select x);

            foreach (var commandType in commandTypes)
            {
                var command = (CommandBase?)Activator.CreateInstance(commandType);

                if (command != null)
                    s_Commands.Add(command);
                else
                    ConsoleLogger.LogMessage(LogType.Error, "Failed to register command {0}. Could not instantiate it as type {1}", commandType.Name, typeof(CommandBase).Name);
            }

            ConsoleLogger.LogMessage(LogType.Info, "Registered {0} command(s)", s_Commands.Count);
        }

        public static async Task Execute(string query)
        {
            var querySegments = query.Split(' ').ToList();
            var commandName = querySegments[0];

            querySegments.RemoveAt(0);
            var arguments = querySegments.ToArray();

            var command = s_Commands.Find(x => x.Name == commandName);
            if (command != null)
                await command.Execute(new CommandArguments(arguments));
        }

        public static IReadOnlyList<CommandBase> Commands => s_Commands;
        static List<CommandBase> s_Commands = new List<CommandBase>();
    }
}
