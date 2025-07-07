using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ConnectToServer : MonoBehaviour
{
    private Socket _socket;
    private Thread _receiveThread;
    private bool _isConnected = false;
    private readonly ConcurrentQueue<string> _receivedMessages = new ConcurrentQueue<string>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _socket  = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        try
        {
            _socket .Connect(ipPoint);
            _isConnected = true;
            Debug.Log("成功连接到服务器.");
            _receiveThread = new Thread(ReceiveMessages);
            _receiveThread.IsBackground = true; // 设置为后台线程，这样主程序退出时它也会退出
            _receiveThread.Start();
        }
        catch (SocketException e)
        {
            _isConnected = false;
            if (e.ErrorCode == 10061)
                print("服务器拒绝连接");
            else
                print("连接服务器失败" + e.ErrorCode);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (_receivedMessages.TryDequeue(out string message))
        {
            Debug.Log($"从服务器收到消息: {message}");
        }
        // 示例：按下空格键发送消息到服务器
        if (_isConnected && Input.GetKeyDown(KeyCode.Space))
        {
            SendMessageToServer("Hello from Unity Client!");
        }
    }
    private void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        while (_isConnected)
        {
            try
            {
                int receivedDataLength = _socket.Receive(buffer);
                if (receivedDataLength == 0)
                {
                    Debug.Log("服务器已断开连接.");
                    _isConnected = false;
                    break;
                }
                string message = Encoding.UTF8.GetString(buffer, 0, receivedDataLength);
                _receivedMessages.Enqueue(message); // 将收到的消息放入队列
            }
            catch (SocketException e)
            {
                Debug.LogError("接收消息时发生Socket异常: " + e.Message);
                _isConnected = false;
                break; // 出现异常时退出循环
            }
        }
    }

    public void SendMessageToServer(string message)
    {
        if (!_isConnected)
        {
            Debug.LogError("未连接到服务器，无法发送消息。");
            return;
        }
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            _socket.Send(data);
            Debug.Log($"发送消息到服务器: {message}");
        }
        catch (SocketException e)
        {
            Debug.LogError("发送消息失败: " + e.Message);
        }
    }
    // 当对象被销毁时（例如场景切换或关闭应用），确保关闭连接
    void OnDestroy()
    {
        _isConnected = false;

        // 停止接收线程
        if (_receiveThread != null && _receiveThread.IsAlive)
        {
            _receiveThread.Abort(); // 注意：Abort() 已不推荐使用，但在此场景下为简单起见而用
        }

        // 关闭socket
        if (_socket != null && _socket.Connected)
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
    }
}