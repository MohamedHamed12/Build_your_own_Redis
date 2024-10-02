using System;
using System.Net;
using System.Runtime.Caching;
using RedisServerApp.Core;

namespace RedisServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var memoryCache = MemoryCache.Default;
            var commandProcessor = new CommandProcessor(memoryCache);
            var redisServer = new RedisServer(IPAddress.Any, 6379, commandProcessor);

            redisServer.Start();
        }
    }
}
