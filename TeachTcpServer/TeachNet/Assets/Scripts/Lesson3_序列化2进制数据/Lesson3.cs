using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerInfo
{
    public int lev;
    public string name;
    public short atk;
    public bool sex;

    public byte[] GetBytes()
    {
        int indexNum = sizeof(int) + //lev int类型  4
                      sizeof(int) + //代表 name字符串转换成字节数组后 数组的长度 4
                      Encoding.UTF8.GetBytes(name).Length + //字符串具体字节数组的长度
                      sizeof(short) + //atk short类型 2
                      sizeof(bool); //sex bool类型 1

        byte[] playerBytes = new byte[indexNum];
        int index = 0;//从 playerBytes数组中的第几个位置去存储数据

        //等级
        BitConverter.GetBytes(lev).CopyTo(playerBytes, index);
        index += sizeof(int);

        //姓名
        byte[] strBytes = Encoding.UTF8.GetBytes(name);
        int num = strBytes.Length;
        //存储的是姓名转换成字节数组后 字节数组的长度
        BitConverter.GetBytes(num).CopyTo(playerBytes, index);
        index += sizeof(int);
        //存储字符串的字节数组
        strBytes.CopyTo(playerBytes, index);
        index += num;

        //攻击力
        BitConverter.GetBytes(atk).CopyTo(playerBytes, index);
        index += sizeof(short);
        //性别
        BitConverter.GetBytes(sex).CopyTo(playerBytes, index);
        index += sizeof(bool);

        return playerBytes;
    }


    public void Write<T>( T t) where T:struct
    {
        byte[] bytes;
        switch (t)
        {
            case bool b:
                bytes = BitConverter.GetBytes(b);
                break;
            case int i:
                bytes = BitConverter.GetBytes(i);
                break;
        }
        
    }
}

public class Lesson3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 非字符串类型转字节数组
        //关键类：BitConverter
        //所在命名空间：System
        //主要作用：除字符串的其它常用类型和字节数组相互转换

        byte[] bytes = BitConverter.GetBytes(1);
        #endregion

        #region 知识点二 字符串类型转字节数组
        //关键类：Encoding
        //所在命名空间：System.Text
        //主要作用：将字符串类型和字节数组相互转换，并且决定转换时使用的字符编码类型，网络通信时建议大家使用UTF-8类型
        byte[] byte2 = Encoding.UTF8.GetBytes("的卡萨福利卡决胜巅峰卡视角的副驾驶的");
        #endregion

        #region 知识点三 如何将一个类对象转换为二进制
        //注意：网络通信中我们不能直接使用数据持久化2进制知识点中的
        //BinaryFormatter 2进制格式化类
        //因为客户端和服务器使用的语言可能不一样，BinaryFormatter是C#的序列化规则，和其它语言之间的兼容性不好
        //如果使用它，那么其它语言开发的服务器无法对其进行反序列化
        //我们需要自己来处理将类对象数据序列化为字节数组

        //单纯的转换一个变量为字节数组非常的简单
        //但是我们如何将一个类对象携带的所有信息放入到一个字节数组中呢
        //我们需要做以下几步
        //1.明确字节数组的容量（注意：在确定字符串字节长度时要考虑解析时如何处理）
        PlayerInfo info = new PlayerInfo();
        info.lev = 10;
        info.name = "唐老狮";
        info.atk = 88;
        info.sex = false;
        //得到的 这个Info数据 如果转换成 字节数组 那么字节数组容器需要的容量
        int indexNum = sizeof(int) + //lev int类型  4
                       sizeof(int) + //代表 name字符串转换成字节数组后 数组的长度 4
                       Encoding.UTF8.GetBytes(info.name).Length + //字符串具体字节数组的长度
                       sizeof(short) + //atk short类型 2
                       sizeof(bool); //sex bool类型 1

        //2.申明一个装载信息的字节数组容器
        byte[] playerBytes = new byte[indexNum];

        //3.将对象中的所有信息转为字节数组并放入该容器当中（可以利用数组中的CopeTo方法转存字节数组）
        //CopyTo方法的第二个参数代表 从容器的第几个位置开始存储
        int index = 0;//从 playerBytes数组中的第几个位置去存储数据

        //等级
        BitConverter.GetBytes(info.lev).CopyTo(playerBytes, index);
        index += sizeof(int);

        //姓名
        byte[] strBytes = Encoding.UTF8.GetBytes(info.name);
        int num = strBytes.Length;
        //存储的是姓名转换成字节数组后 字节数组的长度
        BitConverter.GetBytes(num).CopyTo(playerBytes, index);
        index += sizeof(int);
        //存储字符串的字节数组
        strBytes.CopyTo(playerBytes, index);
        index += num;

        //攻击力
        BitConverter.GetBytes(info.atk).CopyTo(playerBytes, index);
        index += sizeof(short);
        //性别
        BitConverter.GetBytes(info.sex).CopyTo(playerBytes, index);
        index += sizeof(bool);
        #endregion

        #region 总结
        //我们对类对象的2进制序列化主要用到的知识点是
        //1.BitConverter转换非字符串的类型的变量为字节数组
        //2.Encoding.UTF8转换字符串类型的变量为字节数组（注意：为了考虑反序列化，我们在转存2进制，序列化字符串之前，先序列化字符串字节数组的长度）

        //转换流程是
        //1.明确字节数组的容量
        //2.申明一个装载信息的字节数组容器
        //3.将对象中的所有信息转为字节数组并放入该容器当中（利用数组中的CopeTo方法转存字节数组）
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
