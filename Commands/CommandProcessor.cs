using System.Runtime.Caching;
using RedisServerApp.Commands;

namespace RedisServerApp.Core
{
    public class CommandProcessor
    {
        private readonly ObjectCache _db;

        public CommandProcessor(ObjectCache db) => _db = db;

        public ICommand GetCommand(string command)
        {
            return command.ToLower() switch
            {
                var cmd when cmd.StartsWith("ping") => new PingCommand(),
                // var cmd when cmd.StartsWith("echo") => new EchoCommand(),
                // var cmd when cmd.StartsWith("set") => new SetCommand(_db),
                // var cmd when cmd.StartsWith("get") => new GetCommand(_db),
                _ => throw new ArgumentException($"Unknown command: {command}"),
            };
        }
    }
}
