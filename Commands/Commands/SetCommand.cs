using System.Runtime.Caching;

namespace RedisServerApp.Commands
{
    public class SetCommand : ICommand
    {
        private ObjectCache _db;

        public SetCommand(ObjectCache db) => _db = db;

        public string Execute(string[] args)
        {
            if (args.Length < 3 || args[0].ToLower() != "set")
                throw new ArgumentException("Expected: set <key> <value> [px <timeout>]");

            var key = args[1];
            var value = args[2];
            var ttl =
                args.Length == 5 && args[3].ToLower() == "px" ? int.Parse(args[4]) : int.MaxValue;

            _db.Set(
                key,
                value,
                DateTimeOffset.Now.AddMilliseconds(
                    ttl == int.MaxValue ? DateTimeOffset.MaxValue.Millisecond : ttl
                )
            );
            return "+OK\r\n";
        }
    }
}
