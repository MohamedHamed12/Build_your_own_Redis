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
        var rdCmd = new RdCommand(0, new());
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
                rdCmd = new RdCommand(0, new());
            }
        }
    }
);
while (true)
{
    var socket = server.AcceptSocket();
    HandleClientAsync(socket);
}

public class RdCommand
{
    public int size { get; set; }
    public List<RdCommandArg> args { get; set; }

    public RdCommand(int size, List<RdCommandArg> args)
    {
        this.size = size;
        this.args = args;
    }

    public void Print()
    {
        var s = new StringBuilder();
        foreach (var arg in args)
        {
            s.Append(arg.Data);
            s.Append(" ");
        }
        Console.WriteLine(s);
    }
}

public class RdCommandArg
{
    public int Length { get; set; }
    public string Data { get; set; }
}
