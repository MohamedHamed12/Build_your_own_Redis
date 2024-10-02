namespace RedisServerApp.Commands
{
    public class PingCommand : ICommand
    {
        public string Execute(string[] args) => "+PONG\r\n";
    }
}
