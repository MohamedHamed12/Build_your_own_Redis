using System.Runtime.Caching;

namespace RedisServerApp.Commands
{
    public class GetCommand : ICommand
    {
        private ObjectCache _db;

        public GetCommand(ObjectCache db) => _db = db;

        public string Execute(string[] args)
        {
            if (args.Length != 2 || args[0].ToLower() != "get")
                throw new ArgumentException("Expected: get <key>");

            var key = args[1];
            var value = _db.Get(key) as string;

            if (string.IsNullOrEmpty(value))
                return "$-1\r\n";

            return $"${value.Length}\r\n{value}\r\n";
        }
    }
}
