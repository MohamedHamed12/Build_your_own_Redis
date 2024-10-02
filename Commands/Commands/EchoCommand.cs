namespace RedisServerApp.Commands
{
    public class EchoCommand : ICommand
    {
        public string Execute(string[] args) => $"+{string.Join(" ", args)}\r\n";
    }
}
