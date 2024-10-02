using System.Net;
using System.Net.Sockets;
using System.Text;

TcpListener server = new TcpListener(IPAddress.Any, 6379);
server.Start();
var pongMSG = "+PONG\r\n";
var pongMSGBuffer = System.Text.Encoding.UTF8.GetBytes(pongMSG);
var storageDict = new Dictionary<string, (string, DateTime?)>();
var HandleClientAsync = new Action<Socket>(
    async (Socket socket) =>
    {
        var recvStr = "";
        while (true)
        {
            try
            {
                var buffer = new byte[1];
                await socket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
                recvStr += System.Text.Encoding.UTF8.GetString(buffer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                recvStr = "";
            }
        }
    }
);
while (true)
{
    var socket = server.AcceptSocket();
    HandleClientAsync(socket);
}
