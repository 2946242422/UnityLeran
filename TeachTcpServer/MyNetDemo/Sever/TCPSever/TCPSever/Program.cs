// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

Console.WriteLine("Hello, World!");
IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
try
{
    socket.Bind(ipEndPoint);
}
catch (Exception e)
{
    Console.WriteLine(e);
    Console.WriteLine("绑定报错" + e.Message);
    throw;
}

socket.Listen(10);
Console.WriteLine("服务端已启动，等待客户端连入...");
Socket socketClient = socket.Accept();
Console.WriteLine($"客户端 {socketClient.RemoteEndPoint} 已连接.");

// 创建一个新线程来接收客户端消息
Thread receiveThread = new Thread(() => ReceiveMessages(socketClient));
receiveThread.Start();

try
{
    // 主线程用于发送消息
    while (true)
    {
        Console.Write("发送给客户端: ");
        string? messageToSend = Console.ReadLine();
        if (!string.IsNullOrEmpty(messageToSend))
        {
            byte[] data = Encoding.UTF8.GetBytes(messageToSend);
            socketClient.Send(data);
        }
    }
}
catch (SocketException ex)
{
    Console.WriteLine($"与客户端的连接出现问题: {ex.Message}");
}
finally
{
    socketClient.Close();
    socket.Close();
}


// 接收消息的方法
static void ReceiveMessages(Socket client)
{
    byte[] buffer = new byte[1024]; // 缓冲区
    try
    {
        while (true)
        {
            int receivedDataLength = client.Receive(buffer);
            if (receivedDataLength == 0) // 当Receive返回0时，表示客户端已正常断开连接
            {
                Console.WriteLine("客户端已断开连接.");
                client.Close();
                break;
            }
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedDataLength);
            Console.WriteLine($"\n收到客户端消息: {receivedMessage}");
            Console.Write("发送给客户端: "); // 重新显示输入提示
        }
    }
    catch (SocketException ex)
    {
        // 10054: 远程主机强迫关闭了一个现有的连接。通常是对方进程崩溃或强制关闭
        if (ex.SocketErrorCode == SocketError.ConnectionReset)
        {
            Console.WriteLine("\n客户端已强制断开连接.");
        }
        else
        {
            Console.WriteLine($"\n接收消息时发生Socket异常: {ex.Message}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"\n处理消息时发生错误: {ex.Message}");
    }
    finally
    {
        if (client.Connected)
        {
            client.Close();
        }
    }
}