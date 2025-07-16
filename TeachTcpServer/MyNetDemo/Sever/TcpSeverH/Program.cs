// See https://aka.ms/new-console-template for more information

using TcpSeverH;
using System.Net.Sockets;
using System.Net;

namespace TcpSeverH;

/// <summary>
/// 服务端程序入口类。
/// </summary>
class Program
{
    /// <summary>
    /// 静态的ServerSocket实例，方便在其他地方（如ClientSocket）进行访问。
    /// </summary>
    public static ServerSocket socket;
    
    /// <summary>
    /// 程序主入口方法。
    /// </summary>
    /// <param name="args">命令行参数。</param>
    static void Main(string[] args)
    {
        // 1. 创建并初始化服务器Socket对象
        socket = new ServerSocket();
        // 2. 启动服务器，监听本地127.0.0.1的8888端口，最大连接数5000
        socket.Start("127.0.0.1", 8888, 5000);

        Console.WriteLine("服务器启动成功");
        Console.WriteLine("输入 'quit' 关闭服务器。");
        Console.WriteLine("输入 'send <ID> <message>' 向指定客户端发送消息。");
        
        // 3. 阻塞主线程，让服务器持续运行。
        // 同时，提供一个命令 "quit" 来优雅地关闭服务器。
        while (true)
        {
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            if(input == "quit")
            {
                // 调用Close方法，会停止所有后台线程并断开所有连接
                socket.Close();
                break;
            }
            // 增加一个新的命令来处理发送逻辑
            else if(input.StartsWith("send "))
            {
                // 命令格式: "send clientID message"
                string[] parts = input.Split(' ');
                if (parts.Length >= 3)
                {
                    // 尝试将命令的第二部分解析为整数ID
                    if (int.TryParse(parts[1], out int clientID))
                    {
                        // 将命令的第三部分及之后的所有内容合并为消息内容
                        string messageContent = string.Join(" ", parts, 2, parts.Length - 2);
                        
                        // 创建一个 PlayerMsg 消息实例来发送
                        PlayerMsg msg = new PlayerMsg();
                        msg.playerID = 0; // 这个ID可以由服务器定义，比如0代表系统消息
                        msg.playerData = new PlayerData
                        {
                            name = messageContent,
                            atk = 0,
                            lev = 0
                        };

                        // 调用我们新添加的方法
                        socket.SendToClient(clientID, msg);
                        Console.WriteLine($"已向客户端 {clientID} 发送消息: {messageContent}");
                    }
                    else
                    {
                        Console.WriteLine("无效的客户端ID。格式: send <ID> <message>");
                    }
                }
                else
                {
                    Console.WriteLine("无效的命令格式。格式: send <ID> <message>");
                }
            }
        }
    }
}

