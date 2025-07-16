using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

/// <summary>
/// Unity客户端的网络管理器，采用单例模式。
/// 负责与服务器建立连接、收发消息、处理心跳以及分包黏包等网络问题。
/// </summary>
public class NetMgr : MonoBehaviour
{
    /// <summary>
    /// 单例实例，方便在游戏的任何地方访问网络功能。
    /// </summary>
    private static NetMgr instance;

    /// <summary>
    /// 公共的静态属性，用于获取单例实例。
    /// </summary>
    public static NetMgr Instance => instance;

    /// <summary>
    /// 客户端的核心Socket对象。
    /// </summary>
    private Socket socket;
    
    /// <summary>
    /// 发送消息队列（线程安全的生产者-消费者模型）。
    /// Unity主线程（如UI事件）是生产者，调用Send()方法将消息放入此队列。
    /// 后台发送线程是消费者，从此队列中取出消息并发送。
    /// </summary>
    private Queue<BaseMsg> sendMsgQueue = new Queue<BaseMsg>();
    
    /// <summary>
    /// 接收消息队列（线程安全的生产者-消费者模型）。
    /// </summary>
    private Queue<BaseMsg> receiveQueue = new Queue<BaseMsg>();

    /// <summary>
    /// 从Socket中接收到的原始字节流会先暂存到这里，等待后续处理。
    /// </summary>
    private byte[] cacheBytes = new byte[1024 * 1024];
    
    /// <summary>
    /// 缓存区中当前已有数据的字节数。
    /// </summary>
    private int cacheNum = 0;

    /// <summary>
    /// 标记当前是否处于连接状态。
    /// 这个标志位控制着收发线程的循环是否继续执行。
    /// </summary>
    private bool isConnected = false;

    /// <summary>
    /// 心跳消息的发送间隔时间（秒）。
    /// </summary>
    private const int SEND_HEART_MSG_TIME = 2;
    
    /// <summary>
    /// 预先创建好的心跳消息对象，避免重复创建。
    /// </summary>
    private HeartMsg hearMsg = new HeartMsg();

    /// <summary>
    /// Unity生命周期方法：在对象加载时调用。
    /// </summary>
    void Awake()
    {
        instance = this;
        // 让该网络管理对象在切换场景时不会被销毁。
        DontDestroyOnLoad(this.gameObject);
        // 使用Unity的InvokeRepeating功能，每隔SEND_HEART_MSG_TIME秒调用一次SendHeartMsg方法。
        // 这是一种在主线程中实现定时任务的简便方法。
        InvokeRepeating("SendHeartMsg", 0, SEND_HEART_MSG_TIME);
    }

    /// <summary>
    /// 定时发送心跳消息。
    /// </summary>
    private void SendHeartMsg()
    {
        // 只有在连接状态下才发送心跳包
        if (isConnected)
            Send(hearMsg);
    }

    /// <summary>
    /// Unity生命周期方法：每帧调用。
    /// 这是处理服务器消息的唯一入口，确保所有游戏逻辑都在Unity主线程中执行。
    /// </summary>
    void Update()
    {
        // 如果接收队列中有消息，就取出来处理。
        if(receiveQueue.Count > 0)
        {
            BaseMsg msg = receiveQueue.Dequeue();
            // 根据消息类型，分发给不同的模块进行处理
            // 这里只是一个简单的示例，打印收到的玩家信息
            if(msg is PlayerMsg)
            {
                PlayerMsg playerMsg = (msg as PlayerMsg);
                Debug.Log("收到玩家消息:");
                Debug.Log("  ID: " + playerMsg.playerID);
                Debug.Log("  Name: " + playerMsg.playerData.name);
                Debug.Log("  Level: " + playerMsg.playerData.lev);
                Debug.Log("  Attack: " + playerMsg.playerData.atk);
            }
        }
    }

    /// <summary>
    /// 连接到服务器。
    /// </summary>
    /// <param name="ip">服务器IP地址。</param>
    /// <param name="port">服务器端口号。</param>
    public void Connect(string ip, int port)
    {
        // 如果已经连接，则直接返回，防止重复连接。
        if (isConnected)
            return;

        // 初始化Socket
        if (socket == null)
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            // 发起连接请求，这是一个同步方法，会阻塞直到连接成功或失败。
            socket.Connect(ipPoint);
            isConnected = true;
            Debug.Log("连接服务器成功！");
            
            // 连接成功后，开启独立的线程来负责消息的发送和接收，避免阻塞主线程。
            ThreadPool.QueueUserWorkItem(SendMsg);
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }
        catch (SocketException e)
        {
            // 处理连接异常
            if (e.ErrorCode == 10061) // 常见错误码：服务器主动拒绝连接。
                Debug.LogError("服务器拒绝连接");
            else
                Debug.LogError("连接失败，错误码: " + e.ErrorCode + " " + e.Message);
        }
    }

    /// <summary>
    /// 公共的发送消息接口。游戏内其他模块通过此方法发送消息。
    /// 它只是将消息放入队列，真正的发送操作在SendMsg线程中完成。
    /// </summary>
    /// <param name="msg">要发送的消息对象。</param>
    public void Send(BaseMsg msg)
    {
        // 只有在连接状态下才能发送消息
        if(isConnected && socket != null)
            sendMsgQueue.Enqueue(msg);
    }

    /// <summary>
    /// 在独立的后台线程中循环发送消息。
    /// </summary>
    /// <param name="obj">线程池要求的方法参数，此处未使用。</param>
    private void SendMsg(object obj)
    {
        // 只要处于连接状态，就不断循环
        while (isConnected)
        {
            // 如果发送队列里有消息
            if (sendMsgQueue.Count > 0)
            {
                try
                {
                    // 取出消息，序列化后发送
                    socket.Send(sendMsgQueue.Dequeue().Writing());
                }
                catch (Exception e)
                {
                    Debug.LogError("发送消息失败: " + e.Message);
                    // 发送异常通常意味着连接已断开
                    Close();
                }
            }
        }
    }

    /// <summary>
    /// 在独立的后台线程中循环接收消息。
    /// </summary>
    /// <param name="obj">线程池要求的方法参数，此处未使用。</param>
    private void ReceiveMsg(object obj)
    {
        while (isConnected)
        {
            try
            {
                // 检查Socket的缓冲区是否有可读数据
                if(socket.Available > 0)
                {
                    byte[] receiveBytes = new byte[1024 * 5];
                    int receiveNum = socket.Receive(receiveBytes);
                    // 将收到的原始字节流交给HandleReceiveMsg处理分包黏包问题
                    HandleReceiveMsg(receiveBytes, receiveNum);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("接收消息失败: " + e.Message);
                // 接收异常通常意味着连接已断开
                Close();
            }
        }
    }

    /// <summary>
    /// 处理接收到的原始字节流，解决TCP的分包和黏包问题。
    /// 逻辑与服务器端完全一致。
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
            msgLength = -1;
            // 3. 判断缓存中的数据是否足够解析出消息头（ID+Length，共8字节）
            if(cacheNum - nowIndex >= 8)
            {
                msgID = BitConverter.ToInt32(cacheBytes, nowIndex);
                nowIndex += 4;
                msgLength = BitConverter.ToInt32(cacheBytes, nowIndex);
                nowIndex += 4;
            }

            // 4. 如果消息头解析成功，并且缓存中的数据足够解析出完整的消息体
            if(cacheNum - nowIndex >= msgLength && msgLength != -1)
            {
                BaseMsg baseMsg = null;
                // 5. 根据消息ID，反序列化消息体
                switch (msgID)
                {
                    case 1001: // 玩家消息
                        PlayerMsg msg = new PlayerMsg();
                        msg.Reading(cacheBytes, nowIndex);
                        baseMsg = msg;
                        break;
                    // TODO: 在这里可以添加对其他从服务器收到的消息的处理
                }
                // 6. 将解析出的完整消息放入接收队列，等待主线程处理
                if (baseMsg != null)
                    receiveQueue.Enqueue(baseMsg);
                
                // 7. 更新索引，跳过已处理的消息体
                nowIndex += msgLength;

                // 8. 如果所有缓存数据都处理完了，重置缓存计数并退出循环
                if (nowIndex == cacheNum)
                {
                    cacheNum = 0;
                    break;
                }
            }
            // 9. 数据不够一个完整包（分包情况）
            else
            {
                // 如果成功解析了消息头但包体不完整，需要将索引回退
                if (msgLength != -1)
                    nowIndex -= 8;
                
                // 10. 将剩余未处理的数据移动到缓存区的开头，等待下次接收
                Array.Copy(cacheBytes, nowIndex, cacheBytes, 0, cacheNum - nowIndex);
                cacheNum = cacheNum - nowIndex;
                break;
            }
        }
    }

    /// <summary>
    /// 关闭客户端连接。
    /// </summary>
    public void Close()
    {
        if (socket != null && isConnected)
        {
            Debug.Log("客户端主动断开连接");
            
            // 最佳实践：在关闭前，应先向服务器发送一个“我要退出”的消息。
            // 这样服务器可以立即知道你是有意断开，而不是网络问题。
            QuitMsg msg = new QuitMsg();
            try {
                 socket.Send(msg.Writing());
            } catch (Exception e) {
                Debug.LogError("发送退出消息失败: " + e.Message);
            }
            
            // 设置连接状态为false，这将使收发线程的循环终止。
            isConnected = false;

            // 等待一小段时间，让线程有时间退出循环
            Thread.Sleep(100);

            // 安全地关闭和释放Socket资源
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
            
            // 清空队列
            sendMsgQueue.Clear();
            receiveQueue.Clear();
        }
    }

    /// <summary>
    /// Unity生命周期方法：当挂载此脚本的GameObject被销毁时调用。
    /// 确保在游戏退出或场景切换导致对象销毁时，能正确关闭网络连接。
    /// </summary>
    private void OnDestroy()
    {
        Close();
    }
}
