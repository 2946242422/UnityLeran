using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

public class Lesson4 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 字节数组转非字符串类型
        //关键类：BitConverter
        //所在命名空间：System
        //主要作用：除字符串的其它常用类型和字节数组相互转换
        byte[] bytes = BitConverter.GetBytes(99);
        int i = BitConverter.ToInt32(bytes, 0);
        print(i);
        #endregion

        #region 知识点二 字节数组转字符串类型
        //关键类：Encoding
        //所在命名空间：System.Text
        //主要作用：将字符串类型和字节数组相互转换，并且决定转换时使用的字符编码类型，网络通信时建议大家使用UTF-8类型
        byte[] bytes2 = Encoding.UTF8.GetBytes("123123空间大撒了房间阿斯利康放大镜");
        string str = Encoding.UTF8.GetString(bytes2, 0, bytes2.Length);
        print(str);
        #endregion

        #region 知识点三 如何将二进制数据转为一个类对象
        //1.获取到对应的字节数组
        PlayerInfo info = new PlayerInfo();
        info.lev = 10;
        info.name = "唐老狮";
        info.atk = 88;
        info.sex = false;

        byte[] playerBytes = info.GetBytes();

        //2.将字节数组按照序列化时的顺序进行反序列化(将对应字节分组转换为对应类型变量)
        PlayerInfo info2 = new PlayerInfo();
        //等级
        int index = 0;
        info2.lev = BitConverter.ToInt32(playerBytes, index);
        index += 4;
        print(info2.lev);
        //姓名的长度
        int length = BitConverter.ToInt32(playerBytes, index);
        index += 4;
        //姓名字符串
        info2.name = Encoding.UTF8.GetString(playerBytes, index, length);
        index += length;
        print(info2.name);
        //攻击力
        info2.atk = BitConverter.ToInt16(playerBytes, index);
        index += 2;
        print(info2.atk);
        //性别
        info2.sex = BitConverter.ToBoolean(playerBytes, index);
        index += 1;
        print(info2.sex);
        #endregion

        #region 总结
        //我们对类对象的2进制反序列化主要用到的知识点是
        //1.BitConverter转换字节数组为非字符串的类型的变量
        //2.Encoding.UTF8转换字节数组为字符串类型的变量（注意：先读长度，再读字符串）

        //转换流程是
        //1.获取到对应的字节数组
        //2.将字节数组按照序列化时的顺序进行反序列化(将对应字节分组转换为对应类型变量)
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public T Read<T>(byte[] bytes, ref int index) where T:struct
    {
        if(typeof(T) == typeof(int))
        {
            int value = BitConverter.ToInt32(bytes, index);
            index += 4;
            return GetValue<T>(value);
        }


        return default(T);
    }

    private T GetValue<T>(object value)
    {
        return (T)Convert.ChangeType(value, typeof(T));
    }
}
