using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchoRelay.CLI.ConsoleCommands
{
    public class CommandArguments
    {
        public CommandArguments(string[] args)
        {
            Arguments = args;
        }

        public T GetParameter<T>(string paramName, T defaultValue = default!)
        {
            var index = IndexOfParameter(paramName);
            if (index == -1) return defaultValue;

            string value = Arguments[index + 1];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter != null)
                return (T)converter.ConvertFromString(value)!;
            return defaultValue;
        }

        public bool HasParameter(string param)
            => IndexOfParameter(param) > -1;

        private int IndexOfParameter(string param)
        {
            for (int i = 0; i < Arguments.Length; i++)
                if (Arguments[i].Equals(string.Format("-{0}", param), StringComparison.OrdinalIgnoreCase))
                    return i;
            return -1;
        }

        public string[] Arguments { get; set; }
    }

    public abstract class CommandBase
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract Task Execute(CommandArguments args);
    }
}
