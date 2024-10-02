namespace RedisServerApp.Core
{
    public class CommandException : Exception
    {
        public CommandException(string message)
            : base(message) { }
    }

    public class InvalidCommandException : CommandException
    {
        public InvalidCommandException(string command)
            : base($"Invalid command: {command}") { }
    }

    public class ArgumentLengthException : CommandException
    {
        public ArgumentLengthException(string expected)
            : base($"Wrong number of arguments, expected: {expected}") { }
    }
}
