using System.Diagnostics;
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
            // var ttl =
            //     args.Length == 5 && args[3].ToLower() == "px" ? int.Parse(args[4]) : int.MaxValue;

            var ttl = int.MaxValue;
            DateTimeOffset expirationTime =
                ttl == int.MaxValue
                    ? DateTimeOffset.MaxValue
                    : DateTimeOffset.Now.AddMilliseconds(ttl);

            _db.Set(key, value, expirationTime);
            Debug.Assert(_db.Contains(key), "Failed to set the key in the cache.");
            Debug.Assert(
                _db.Get(key)?.Equals(value) == true,
                "The value in the cache does not match the expected value."
            );
            return "+OK\r\n";
        }
    }
}
