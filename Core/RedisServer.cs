using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RedisServerApp.Core
{
    public class RedisServer
    {
        private readonly TcpListener _server;
        private readonly CommandProcessor _commandProcessor;

        public RedisServer(IPAddress ipAddress, int port, CommandProcessor commandProcessor)
        {
            _server = new TcpListener(ipAddress, port);
            _commandProcessor = commandProcessor;
        }

        public void Start()
        {
            _server.Start();
            Console.WriteLine("Redis server started...");

            while (true)
            {
                var client = _server.AcceptTcpClient();
                var clientHandler = new ClientHandler(client, _commandProcessor);
                new Thread(clientHandler.Handle).Start();
            }
        }
    }
}
