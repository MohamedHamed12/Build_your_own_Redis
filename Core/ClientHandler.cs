using System.Net.Sockets;
using System.Text;

namespace RedisServerApp.Core
{
    public class ClientHandler
    {
        private readonly TcpClient _client;
        private readonly CommandProcessor _commandProcessor;

        public ClientHandler(TcpClient client, CommandProcessor commandProcessor)
        {
            _client = client;
            _commandProcessor = commandProcessor;
        }

        public void Handle()
        {
            using var stream = _client.GetStream();
            var buffer = new byte[1024];
            while (true)
            {
                var bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                    break;

                var recvStr = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();
                var args = recvStr.Split(" ");
                var command = _commandProcessor.GetCommand(args[0]);

                var response = command.Execute(args);
                var responseBytes = Encoding.ASCII.GetBytes(response);

                stream.Write(responseBytes, 0, responseBytes.Length);
            }
        }
    }
}
