using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

/// <summary>
/// Unity�ͻ��˵���������������õ���ģʽ��
/// ������������������ӡ��շ���Ϣ�����������Լ��ְ������������⡣
/// </summary>
public class NetMgr : MonoBehaviour
{
    /// <summary>
    /// ����ʵ������������Ϸ���κεط��������繦�ܡ�
    /// </summary>
    private static NetMgr instance;

    /// <summary>
    /// �����ľ�̬���ԣ����ڻ�ȡ����ʵ����
    /// </summary>
    public static NetMgr Instance => instance;

    /// <summary>
    /// �ͻ��˵ĺ���Socket����
    /// </summary>
    private Socket socket;
    
    /// <summary>
    /// ������Ϣ���У��̰߳�ȫ��������-������ģ�ͣ���
    /// Unity���̣߳���UI�¼����������ߣ�����Send()��������Ϣ����˶��С�
    /// ��̨�����߳��������ߣ��Ӵ˶�����ȡ����Ϣ�����͡�
    /// </summary>
    private Queue<BaseMsg> sendMsgQueue = new Queue<BaseMsg>();
    
    /// <summary>
    /// ������Ϣ���У��̰߳�ȫ��������-������ģ�ͣ���
    /// </summary>
    private Queue<BaseMsg> receiveQueue = new Queue<BaseMsg>();

    /// <summary>
    /// ��Socket�н��յ���ԭʼ�ֽ��������ݴ浽����ȴ���������
    /// </summary>
    private byte[] cacheBytes = new byte[1024 * 1024];
    
    /// <summary>
    /// �������е�ǰ�������ݵ��ֽ�����
    /// </summary>
    private int cacheNum = 0;

    /// <summary>
    /// ��ǵ�ǰ�Ƿ�������״̬��
    /// �����־λ�������շ��̵߳�ѭ���Ƿ����ִ�С�
    /// </summary>
    private bool isConnected = false;

    /// <summary>
    /// ������Ϣ�ķ��ͼ��ʱ�䣨�룩��
    /// </summary>
    private const int SEND_HEART_MSG_TIME = 2;
    
    /// <summary>
    /// Ԥ�ȴ����õ�������Ϣ���󣬱����ظ�������
    /// </summary>
    private HeartMsg hearMsg = new HeartMsg();

    /// <summary>
    /// Unity�������ڷ������ڶ������ʱ���á�
    /// </summary>
    void Awake()
    {
        instance = this;
        // �ø��������������л�����ʱ���ᱻ���١�
        DontDestroyOnLoad(this.gameObject);
        // ʹ��Unity��InvokeRepeating���ܣ�ÿ��SEND_HEART_MSG_TIME�����һ��SendHeartMsg������
        // ����һ�������߳���ʵ�ֶ�ʱ����ļ�㷽����
        InvokeRepeating("SendHeartMsg", 0, SEND_HEART_MSG_TIME);
    }

    /// <summary>
    /// ��ʱ����������Ϣ��
    /// </summary>
    private void SendHeartMsg()
    {
        // ֻ��������״̬�²ŷ���������
        if (isConnected)
            Send(hearMsg);
    }

    /// <summary>
    /// Unity�������ڷ�����ÿ֡���á�
    /// ���Ǵ����������Ϣ��Ψһ��ڣ�ȷ��������Ϸ�߼�����Unity���߳���ִ�С�
    /// </summary>
    void Update()
    {
        // ������ն���������Ϣ����ȡ��������
        if(receiveQueue.Count > 0)
        {
            BaseMsg msg = receiveQueue.Dequeue();
            // ������Ϣ���ͣ��ַ�����ͬ��ģ����д���
            // ����ֻ��һ���򵥵�ʾ������ӡ�յ��������Ϣ
            if(msg is PlayerMsg)
            {
                PlayerMsg playerMsg = (msg as PlayerMsg);
                Debug.Log("�յ������Ϣ:");
                Debug.Log("  ID: " + playerMsg.playerID);
                Debug.Log("  Name: " + playerMsg.playerData.name);
                Debug.Log("  Level: " + playerMsg.playerData.lev);
                Debug.Log("  Attack: " + playerMsg.playerData.atk);
            }
        }
    }

    /// <summary>
    /// ���ӵ���������
    /// </summary>
    /// <param name="ip">������IP��ַ��</param>
    /// <param name="port">�������˿ںš�</param>
    public void Connect(string ip, int port)
    {
        // ����Ѿ����ӣ���ֱ�ӷ��أ���ֹ�ظ����ӡ�
        if (isConnected)
            return;

        // ��ʼ��Socket
        if (socket == null)
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            // ����������������һ��ͬ��������������ֱ�����ӳɹ���ʧ�ܡ�
            socket.Connect(ipPoint);
            isConnected = true;
            Debug.Log("���ӷ������ɹ���");
            
            // ���ӳɹ��󣬿����������߳���������Ϣ�ķ��ͺͽ��գ������������̡߳�
            ThreadPool.QueueUserWorkItem(SendMsg);
            ThreadPool.QueueUserWorkItem(ReceiveMsg);
        }
        catch (SocketException e)
        {
            // ���������쳣
            if (e.ErrorCode == 10061) // ���������룺�����������ܾ����ӡ�
                Debug.LogError("�������ܾ�����");
            else
                Debug.LogError("����ʧ�ܣ�������: " + e.ErrorCode + " " + e.Message);
        }
    }

    /// <summary>
    /// �����ķ�����Ϣ�ӿڡ���Ϸ������ģ��ͨ���˷���������Ϣ��
    /// ��ֻ�ǽ���Ϣ������У������ķ��Ͳ�����SendMsg�߳�����ɡ�
    /// </summary>
    /// <param name="msg">Ҫ���͵���Ϣ����</param>
    public void Send(BaseMsg msg)
    {
        // ֻ��������״̬�²��ܷ�����Ϣ
        if(isConnected && socket != null)
            sendMsgQueue.Enqueue(msg);
    }

    /// <summary>
    /// �ڶ����ĺ�̨�߳���ѭ��������Ϣ��
    /// </summary>
    /// <param name="obj">�̳߳�Ҫ��ķ����������˴�δʹ�á�</param>
    private void SendMsg(object obj)
    {
        // ֻҪ��������״̬���Ͳ���ѭ��
        while (isConnected)
        {
            // ������Ͷ���������Ϣ
            if (sendMsgQueue.Count > 0)
            {
                try
                {
                    // ȡ����Ϣ�����л�����
                    socket.Send(sendMsgQueue.Dequeue().Writing());
                }
                catch (Exception e)
                {
                    Debug.LogError("������Ϣʧ��: " + e.Message);
                    // �����쳣ͨ����ζ�������ѶϿ�
                    Close();
                }
            }
        }
    }

    /// <summary>
    /// �ڶ����ĺ�̨�߳���ѭ��������Ϣ��
    /// </summary>
    /// <param name="obj">�̳߳�Ҫ��ķ����������˴�δʹ�á�</param>
    private void ReceiveMsg(object obj)
    {
        while (isConnected)
        {
            try
            {
                // ���Socket�Ļ������Ƿ��пɶ�����
                if(socket.Available > 0)
                {
                    byte[] receiveBytes = new byte[1024 * 5];
                    int receiveNum = socket.Receive(receiveBytes);
                    // ���յ���ԭʼ�ֽ�������HandleReceiveMsg����ְ�������
                    HandleReceiveMsg(receiveBytes, receiveNum);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("������Ϣʧ��: " + e.Message);
                // �����쳣ͨ����ζ�������ѶϿ�
                Close();
            }
        }
    }

    /// <summary>
    /// ������յ���ԭʼ�ֽ��������TCP�ķְ��������⡣
    /// �߼������������ȫһ�¡�
    /// </summary>
    /// <param name="receiveBytes">���δ�Socket�ж�ȡ�����ֽ����顣</param>
    /// <param name="receiveNum">���ζ�ȡ�����ֽ�����</param>
    private void HandleReceiveMsg(byte[] receiveBytes, int receiveNum)
    {
        int msgID = 0;
        int msgLength = 0;
        int nowIndex = 0;

        // 1. �����յ�������ƴ�ӵ���һ��δ����������ݣ����棩����
        receiveBytes.CopyTo(cacheBytes, cacheNum);
        cacheNum += receiveNum;

        // 2. ѭ��������Ϊһ�ο��ܻ��յ���������İ�
        while (true)
        {
            msgLength = -1;
            // 3. �жϻ����е������Ƿ��㹻��������Ϣͷ��ID+Length����8�ֽڣ�
            if(cacheNum - nowIndex >= 8)
            {
                msgID = BitConverter.ToInt32(cacheBytes, nowIndex);
                nowIndex += 4;
                msgLength = BitConverter.ToInt32(cacheBytes, nowIndex);
                nowIndex += 4;
            }

            // 4. �����Ϣͷ�����ɹ������һ����е������㹻��������������Ϣ��
            if(cacheNum - nowIndex >= msgLength && msgLength != -1)
            {
                BaseMsg baseMsg = null;
                // 5. ������ϢID�������л���Ϣ��
                switch (msgID)
                {
                    case 1001: // �����Ϣ
                        PlayerMsg msg = new PlayerMsg();
                        msg.Reading(cacheBytes, nowIndex);
                        baseMsg = msg;
                        break;
                    // TODO: �����������Ӷ������ӷ������յ�����Ϣ�Ĵ���
                }
                // 6. ����������������Ϣ������ն��У��ȴ����̴߳���
                if (baseMsg != null)
                    receiveQueue.Enqueue(baseMsg);
                
                // 7. ���������������Ѵ������Ϣ��
                nowIndex += msgLength;

                // 8. ������л������ݶ��������ˣ����û���������˳�ѭ��
                if (nowIndex == cacheNum)
                {
                    cacheNum = 0;
                    break;
                }
            }
            // 9. ���ݲ���һ�����������ְ������
            else
            {
                // ����ɹ���������Ϣͷ�����岻��������Ҫ����������
                if (msgLength != -1)
                    nowIndex -= 8;
                
                // 10. ��ʣ��δ����������ƶ����������Ŀ�ͷ���ȴ��´ν���
                Array.Copy(cacheBytes, nowIndex, cacheBytes, 0, cacheNum - nowIndex);
                cacheNum = cacheNum - nowIndex;
                break;
            }
        }
    }

    /// <summary>
    /// �رտͻ������ӡ�
    /// </summary>
    public void Close()
    {
        if (socket != null && isConnected)
        {
            Debug.Log("�ͻ��������Ͽ�����");
            
            // ���ʵ�����ڹر�ǰ��Ӧ�������������һ������Ҫ�˳�������Ϣ��
            // ������������������֪����������Ͽ����������������⡣
            QuitMsg msg = new QuitMsg();
            try {
                 socket.Send(msg.Writing());
            } catch (Exception e) {
                Debug.LogError("�����˳���Ϣʧ��: " + e.Message);
            }
            
            // ��������״̬Ϊfalse���⽫ʹ�շ��̵߳�ѭ����ֹ��
            isConnected = false;

            // �ȴ�һС��ʱ�䣬���߳���ʱ���˳�ѭ��
            Thread.Sleep(100);

            // ��ȫ�عرպ��ͷ�Socket��Դ
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
            
            // ��ն���
            sendMsgQueue.Clear();
            receiveQueue.Clear();
        }
    }

    /// <summary>
    /// Unity�������ڷ����������ش˽ű���GameObject������ʱ���á�
    /// ȷ������Ϸ�˳��򳡾��л����¶�������ʱ������ȷ�ر��������ӡ�
    /// </summary>
    private void OnDestroy()
    {
        Close();
    }
}
