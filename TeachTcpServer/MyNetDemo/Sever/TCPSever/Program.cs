// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using System.Text;

public class MultiClientServer
{
    // 存储所有已连接客户端的Socket
    private static List<Socket> clientSockets = new List<Socket>();
    // 线程锁，用于在多线程环境下安全地操作clientSockets列表
    private static readonly object _lock = new object();

    public static void Main(string[] args)
    {
        Console.WriteLine("服务端已启动...");
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8080); // 监听所有网络接口
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        serverSocket.Bind(ipEndPoint);
        serverSocket.Listen(100); // 增加监听队列长度

        // 将接受客户端连接的操作放到一个独立的线程中，以免阻塞主线程
        Thread acceptThread = new Thread(() => AcceptClients(serverSocket));
        acceptThread.IsBackground = true;
        acceptThread.Start();

        Console.WriteLine("输入消息并按回车，即可向所有客户端广播。输入 'exit' 关闭服务器。");

        // 主线程用于读取控制台输入并广播消息
        while (true)
        {
            string? message = Console.ReadLine();
            if (message?.ToLower() == "exit")
            {
                break;
            }
            if (!string.IsNullOrEmpty(message))
            {
                BroadcastMessage(message);
            }
        }

        // 关闭所有连接和服务器
        Console.WriteLine("正在关闭服务器...");
        lock (_lock)
        {
            foreach (var client in clientSockets)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            clientSockets.Clear();
        }
        serverSocket.Close();
    }

    /// <summary>
    /// 循环接受客户端连接
    /// </summary>
    private static void AcceptClients(Socket serverSocket)
    {
        while (true)
        {
            try
            {
                Socket clientSocket = serverSocket.Accept();
                Console.WriteLine($"客户端 {clientSocket.RemoteEndPoint} 已连接. 当前连接数: {clientSockets.Count + 1}");

                // 加锁以安全地将新客户端添加到列表
                lock (_lock)
                {
                    clientSockets.Add(clientSocket);
                }

                // 为每个客户端创建一个新的线程来处理消息接收
                Thread receiveThread = new Thread(() => ReceiveMessages(clientSocket));
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"接受客户端连接时出错: {ex.Message}");
                break;
            }
        }
    }

    /// <summary>
    /// 从指定客户端接收消息
    /// </summary>
    private static void ReceiveMessages(Socket clientSocket)
    {
        byte[] buffer = new byte[1024];
        try
        {
            while (true)
            {
                int receivedDataLength = clientSocket.Receive(buffer);
                if (receivedDataLength == 0)
                {
                    // 客户端正常断开
                    HandleClientDisconnect(clientSocket);
                    break;
                }
                string message = Encoding.UTF8.GetString(buffer, 0, receivedDataLength);
                Console.WriteLine($"收到来自 {clientSocket.RemoteEndPoint} 的消息: {message}");

                // 可选：将收到的消息转发给其他所有客户端
                // string forwardMessage = $"{clientSocket.RemoteEndPoint} 说: {message}";
                // BroadcastMessage(forwardMessage, clientSocket);
            }
        }
        catch (SocketException)
        {
            // 客户端异常断开
            HandleClientDisconnect(clientSocket);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"处理来自 {clientSocket.RemoteEndPoint} 的消息时发生错误: {ex.Message}");
            HandleClientDisconnect(clientSocket);
        }
    }
    
    /// <summary>
    /// 处理客户端断开连接
    /// </summary>
    private static void HandleClientDisconnect(Socket clientSocket)
    {
        // 确保Socket是有效的并且在列表中
        if (clientSocket == null) return;

        Console.WriteLine($"客户端 {clientSocket.RemoteEndPoint} 已断开连接.");
        
        lock (_lock)
        {
            if (clientSockets.Contains(clientSocket))
            {
                clientSockets.Remove(clientSocket);
            }
        }
        try
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
        catch (Exception)
        {
            // 忽略关闭时可能发生的异常
        }
    }


    /// <summary>
    /// 向所有连接的客户端广播消息
    /// </summary>
    /// <param name="message">要发送的消息</param>
    /// <param name="excludeClient">（可选）要排除的客户端，通常用于消息转发，避免发回给发送者</param>
    private static void BroadcastMessage(string message, Socket? excludeClient = null)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        Console.WriteLine($"正在广播消息: {message}");
        
        lock (_lock)
        {
            // 创建一个副本进行遍历，以防在遍历时列表被修改
            foreach (var client in clientSockets.ToList()) 
            {
                if (client != excludeClient && client.Connected)
                {
                    try
                    {
                        client.Send(data);
                    }
                    catch (SocketException)
                    {
                        // 如果发送失败，说明该客户端也断开了连接
                        // 这里可以立即处理断连，或者等待其接收线程自己处理
                        Console.WriteLine($"向 {client.RemoteEndPoint} 发送失败，可能已断开。");
                    }
                }
            }
        }
    }
}