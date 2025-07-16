using System.Net.Sockets;

namespace TcpSeverH;

/// <summary>
/// 代表一个连接到服务器的客户端。
/// 负责处理与该客户端相关的所有网络操作，如消息的收发、超时检测等。
/// </summary>
public class ClientSocket
{
    /// <summary>
    /// 用于生成客户端唯一ID的静态计数器。
    /// </summary>
    private static int CLIENT_BEGIN_ID = 1;

    /// <summary>
    /// 当前客户端的唯一标识ID。
    /// </summary>
    public int clientID;
    
    /// <summary>
    /// 与客户端通信的Socket对象。
    /// </summary>
    public Socket socket;

    /// <summary>
    /// 接收数据缓冲区。用于处理分包、黏包问题。当收到的数据不是一个完整的消息时，会暂存到这里。
    /// </summary>
    private byte[] cacheBytes = new byte[1024 * 1024];

    /// <summary>
    /// 缓冲区中当前已有数据的长度。
    /// </summary>
    private int cacheNum = 0;

    /// <summary>
    /// 上一次成功收到消息的时间戳（秒）。用于心跳超时检测。
    /// </summary>
    private long frontTime = -1;

    /// <summary>
    /// 超时时间（秒）。如果超过这个时间没有收到任何消息（特别是心跳包），则认为客户端已断开。
    /// </summary>
    private static int TIME_OUT_TIME = 10;

    /// <summary>
    /// 公共属性，用于判断客户端当前是否处于连接状态。
    /// </summary>
    public bool Connected => socket != null && socket.Connected;

    /// <summary>
    /// 构造函数，在服务器接受新连接时调用。
    /// </summary>
    /// <param name="socket">由服务器Accept方法返回的客户端Socket。</param>
    public ClientSocket(Socket socket)
    {
        this.clientID = CLIENT_BEGIN_ID;
        this.socket = socket;
        ++CLIENT_BEGIN_ID;
        // 初始化心跳时间
        frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
    }

    /// <summary>
    /// 检测客户端是否超时。
    /// 在每次Receive轮询时调用，检查距离上次收到消息是否超过了预设的超时时间。
    /// </summary>
    private void CheckTimeOut()
    {
        if (frontTime != -1 &&
            DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime >= TIME_OUT_TIME)
        {
            Console.WriteLine($"客户端 {clientID} 心跳超时，准备断开。");
            // 如果超时，将自己添加到服务器的待移除列表中
            Program.socket.AddDelSocket(this);
        }
    }

    /// <summary>
    /// 关闭客户端连接，并释放相关资源。
    /// </summary>
    public void Close()
    {
        if (socket != null)
        {
            Console.WriteLine($"关闭客户端 {clientID} 的连接。");
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }

    /// <summary>
    /// 向客户端发送消息。
    /// </summary>
    /// <param name="info">要发送的消息对象，它继承自BaseMsg。</param>
    public void Send(BaseMsg info)
    {
        if (Connected)
        {
            try
            {
                // 调用消息对象的Writing方法，将其序列化为字节数组后发送
                socket.Send(info.Writing());
            }
            catch (Exception e)
            {
                Console.WriteLine("发消息出错:" + e.Message);
                // 发送失败，说明连接可能已断开，将自己添加到待移除列表
                Program.socket.AddDelSocket(this);
            }
        }
        else
        {
            // 如果未连接，直接添加到待移除列表
            Program.socket.AddDelSocket(this);
        }
    }

    /// <summary>
    /// 从客户端接收消息。此方法由ServerSocket的Receive循环调用。
    /// </summary>
    public void Receive()
    {
        if (!Connected)
        {
            Program.socket.AddDelSocket(this);
            return;
        }

        try
        {
            // 1. 检测是否有可读数据
            if (socket.Available > 0)
            {
                byte[] result = new byte[1024 * 5];
                int receiveNum = socket.Receive(result);
                // 2. 处理收到的原始字节数据（分包、黏包）
                HandleReceiveMsg(result, receiveNum);
            }

            // 3. 每次轮询时都检测一次超时
            CheckTimeOut();
        }
        catch (Exception e)
        {
            Console.WriteLine("收消息出错:" + e.Message);
            // 接收消息时出错，也认为连接已断开
            Program.socket.AddDelSocket(this);
        }
    }

    /// <summary>
    /// 处理接收到的原始字节流，解决分包和黏包问题。
    /// 这是一个核心方法，确保能从连续的TCP流中正确解析出一条或多条消息。
    /// </summary>
    /// <param name="receiveBytes">本次从Socket中读取到的字节数组。</param>
    /// <param name="receiveNum">本次读取到的字节数。</param>
    private void HandleReceiveMsg(byte[] receiveBytes, int receiveNum)
    {
        int msgID = 0;
        int msgLength = 0;
        int nowIndex = 0;

        // 1. 将新收到的数据拼接到上一次未处理完的数据（缓存）后面
        receiveBytes.CopyTo(cacheBytes, cacheNum);
        cacheNum += receiveNum;

        // 2. 循环处理，因为一次可能会收到多个完整的包
        while (true)
        {
            // 重置消息长度，防止使用上一次循环的旧值
            msgLength = -1;
            
            // 3. 判断缓存中的数据是否足够解析出消息头（ID+Length，共8字节）
            if (cacheNum - nowIndex >= 8)
            {
                // 解析ID
                msgID = BitConverter.ToInt32(cacheBytes, nowIndex);
                nowIndex += 4;
                // 解析消息体长度
                msgLength = BitConverter.ToInt32(cacheBytes, nowIndex);
                nowIndex += 4;
            }

            // 4. 如果消息头解析成功，并且缓存中的数据足够解析出完整的消息体
            if (cacheNum - nowIndex >= msgLength && msgLength != -1)
            {
                // 5. 根据消息ID，反序列化消息体
                BaseMsg baseMsg = null;
                switch (msgID)
                {
                    case 1001: // 玩家消息
                        baseMsg = new PlayerMsg();
                        baseMsg.Reading(cacheBytes, nowIndex);
                        break;
                    case 1003: // 退出消息
                        baseMsg = new QuitMsg();
                        break;
                    case 999: // 心跳消息
                        baseMsg = new HeartMsg();
                        // 心跳包和退出消息没有消息体，不需要反序列化
                        break;
                }

                // 6. 将解析出的消息交给线程池处理，避免阻塞接收线程
                if (baseMsg != null)
                    ThreadPool.QueueUserWorkItem(MsgHandle, baseMsg);
                
                // 7. 更新索引，跳过已处理的消息体
                nowIndex += msgLength;

                // 8. 如果所有缓存数据都处理完了，重置缓存计数并退出循环
                if (nowIndex == cacheNum)
                {
                    cacheNum = 0;
                    break;
                }
            }
            else
            {
                // 9. 数据不够一个完整包（分包情况）
                // 如果成功解析了消息头但包体不完整，需要将索引回退，因为这8个字节也是下一包的一部分
                if (msgLength != -1)
                    nowIndex -= 8;
                
                // 10. 将剩余未处理的数据（可能是半个包）移动到缓存区的开头
                // 等待下一次接收到数据后，拼接起来继续处理
                Array.Copy(cacheBytes, nowIndex, cacheBytes, 0, cacheNum - nowIndex);
                cacheNum = cacheNum - nowIndex;
                break;
            }
        }
    }
    
    /// <summary>
    /// 在线程池中处理具体的消息逻辑。
    /// </summary>
    /// <param name="obj">由ThreadPool传递过来的BaseMsg对象。</param>
    private void MsgHandle(object obj)
    {
        BaseMsg msg = obj as BaseMsg;
        // 根据消息类型执行不同操作
        if (msg is PlayerMsg)
        {
            PlayerMsg playerMsg = msg as PlayerMsg;
            Console.WriteLine($"收到客户端 {clientID} 的玩家消息:");
            Console.WriteLine("  PlayerID: " + playerMsg.playerID);
            Console.WriteLine("  Name: " + playerMsg.playerData.name);
            Console.WriteLine("  Lev: " + playerMsg.playerData.lev);
            Console.WriteLine("  Atk: " + playerMsg.playerData.atk);
        }
        else if (msg is QuitMsg)
        {
            // 收到客户端发来的退出消息，将其加入待移除列表
            Console.WriteLine($"收到客户端 {clientID} 的退出请求。");
            Program.socket.AddDelSocket(this);
        }
        else if (msg is HeartMsg)
        {
            // 收到心跳消息，更新上一次收到消息的时间
            frontTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            Console.WriteLine($"收到客户端 {clientID} 的心跳消息");
        }
    }
}