namespace RedisServerApp.Commands
{
    public interface ICommand
    {
        string Execute(string[] args);
    }
}
