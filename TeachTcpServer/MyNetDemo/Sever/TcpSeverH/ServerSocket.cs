using System.Net.Sockets;
using System.Net;

namespace TcpSeverH;

/// <summary>
/// 服务器端网络模块，用于管理服务器的启动、客户端的连接以及消息的收发。
/// </summary>
public class ServerSocket
{
    /// <summary>
    /// 服务器的主Socket，用于监听客户端的连接请求。
    /// </summary>
    public Socket socket;
    
    /// <summary>
    /// 用于存储所有已连接的客户端Socket的字典。
    /// 键是客户端的唯一ID，值是ClientSocket对象。
    /// 使用 lock(clientDic) 来保证多线程访问时的线程安全。
    /// </summary>
    public Dictionary<int, ClientSocket> clientDic = new Dictionary<int, ClientSocket>();
    
    /// <summary>
    /// 待移除的客户端Socket列表。
    /// 为避免在遍历clientDic时直接修改它（这会导致异常），
    /// 我们将需要断开的客户端先添加到这个列表中，之后再统一处理。
    /// </summary>
    private List<ClientSocket> delList = new List<ClientSocket>();

    /// <summary>
    /// 服务器是否已关闭的标志。
    /// 当设置为 true 时，Accept 和 Receive 线程中的循环将停止。
    /// </summary>
    private bool isClose;

    /// <summary>
    /// 启动服务器。
    /// </summary>
    /// <param name="ip">要绑定的IP地址。</param>
    /// <param name="port">要监听的端口号。</param>
    /// <param name="num">最大监听队列长度（挂起的连接队列的最大长度）。</param>
    public void Start(string ip, int port, int num)
    {
        isClose = false;
        // 1. 创建服务器Socket
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // 2. 绑定IP和端口
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        socket.Bind(ipPoint);
        // 3. 开始监听
        socket.Listen(num);
        
        // 4. 为了不阻塞主线程，将“接受连接”和“接收消息”的逻辑放到独立的线程中处理。
        // 使用线程池可以更高效地管理线程资源。
        ThreadPool.QueueUserWorkItem(Accept);
        ThreadPool.QueueUserWorkItem(Receive);
    }
    
    /// <summary>
    /// 在一个独立的线程中持续接受客户端的连接请求。
    /// </summary>
    /// <param name="obj">ThreadPool.QueueUserWorkItem 要求的参数，这里未使用。</param>
    private void Accept(object obj)
    {
        // 只要服务器没有关闭，就一直循环等待客户端连接
        while (!isClose)
        {
            try
            {
                // Accept方法会阻塞线程，直到有新的客户端连接进来
                Socket clientSocket = socket.Accept();
                // 为连接上的客户端创建一个包装类 ClientSocket
                ClientSocket client = new ClientSocket(clientSocket);
                // 使用 lock 确保多线程环境下对 clientDic 的访问是安全的
                lock (clientDic)
                {
                    clientDic.Add(client.clientID, client);
                }
                Console.WriteLine($"客户端 {client.clientID} 连接成功");
            }
            catch (Exception e)
            {
                // Accept 过程中出现异常（比如服务器Socket被关闭时），打印错误信息
                Console.WriteLine("客户端连入报错:" + e.Message);
            }
        }
    }
    
    /// <summary>
    /// 在一个独立的线程中持续接收所有已连接客户端发送的消息。
    /// </summary>
    /// <param name="obj">ThreadPool.QueueUserWorkItem 要求的参数，这里未使用。</param>
    private void Receive(object obj)
    {
        // 只要服务器没有关闭，就一直循环
        while (!isClose)
        {
            // 只有当有客户端连接时，才进行接收操作
            if (clientDic.Count > 0)
            {
                lock (clientDic)
                {
                    // 遍历所有客户端，调用其Receive方法来处理消息
                    foreach (ClientSocket client in clientDic.Values)
                    {
                        client.Receive();
                    }

                    // 在每次循环的末尾，处理那些需要断开连接的客户端
                    CloseDelListSocket();
                }
            }
        }
    }
    
    /// <summary>
    /// 关闭服务器，断开所有连接。
    /// </summary>
    public void Close()
    {
        isClose = true;
        // 安全地关闭所有客户端的连接
        lock (clientDic)
        {
            foreach (ClientSocket client in clientDic.Values)
            {
                client.Close();
            }
            clientDic.Clear();
        }

        // 关闭服务器主Socket
        if( socket != null )
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }
    
    /// <summary>
    /// 向所有已连接的客户端广播消息。
    /// </summary>
    /// <param name="info">要广播的消息对象。</param>
    public void Broadcast(BaseMsg info)
    {
        lock (clientDic)
        {
            foreach (ClientSocket client in clientDic.Values)
            {
                client.Send(info);
            }
        }
    }

    /// <summary>
    /// 向指定的客户端发送消息。
    /// </summary>
    /// <param name="clientID">目标客户端的ID。</param>
    /// <param name="info">要发送的消息对象。</param>
    public void SendToClient(int clientID, BaseMsg info)
    {
        lock (clientDic)
        {
            // 使用 TryGetValue 会更安全和高效，避免了两次查找（一次Contains，一次索引）
            if (clientDic.TryGetValue(clientID, out ClientSocket client))
            {
                // 如果找到了客户端，就调用其Send方法
                client.Send(info);
            }
            else
            {
                // 如果字典中没有这个ID，可以打印一个提示
                Console.WriteLine($"发送消息失败：未找到ID为 {clientID} 的客户端。");
            }
        }
    }

    /// <summary>
    /// 将指定的客户端Socket添加到处待移除列表。
    /// 这是一个线程安全的操作，因为它只是添加到列表中，实际的移除操作会在Receive循环的末尾统一进行。
    /// </summary>
    /// <param name="socket">需要被移除的客户端Socket。</param>
    public void AddDelSocket(ClientSocket socket)
    {
        lock (delList)
        {
            if (!delList.Contains(socket))
                delList.Add(socket);
        }
    }
    
    /// <summary>
    /// 处理待移除列表中的所有客户端，关闭它们的连接并从主字典中移除。
    /// </summary>
    public void CloseDelListSocket()
    {
        lock (delList)
        {
            // 遍历待移除列表
            for (int i = 0; i < delList.Count; i++)
            {
                CloseClientSocket(delList[i]);
            }
            delList.Clear();
        }
    }
    
    /// <summary>
    /// 关闭单个客户端连接并将其从clientDic中移除。
    /// </summary>
    /// <param name="socket">要关闭的客户端Socket。</param>
    public void CloseClientSocket(ClientSocket socket)
    {
        lock (clientDic)
        {
            socket.Close();
            if (clientDic.ContainsKey(socket.clientID))
            {
                clientDic.Remove(socket.clientID);
                Console.WriteLine("客户端{0}主动断开连接了", socket.clientID);
            }
        }
    }
}