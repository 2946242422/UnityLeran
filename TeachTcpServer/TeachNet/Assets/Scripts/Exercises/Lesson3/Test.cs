using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TestInfo : BaseData
{
    public short lev;
    public Player p;
    public int hp;
    public string name;
    public bool sex;
    public override int GetBytesNum()
    {
        return sizeof(short) +//2 
            p.GetBytesNum() + //4
            sizeof(int) +//4
            4 + Encoding.UTF8.GetBytes(name).Length + //4+n
            sizeof(bool);//1
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteShort(bytes, lev, ref index);
        WriteData(bytes, p, ref index);
        WriteInt(bytes, hp, ref index);
        WriteString(bytes, name, ref index);
        WriteBool(bytes, sex, ref index);
        //序列化list的长度是多少
        //在循环这个list保存对应的类型
        return bytes;
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        lev = ReadShort(bytes, ref index);//0
        p = ReadData<Player>(bytes, ref index);//2
        hp = ReadInt(bytes, ref index);//6
        name = ReadString(bytes, ref index);//10
        sex = ReadBool(bytes, ref index);//17
        //反序列化出list的长度
        //循环反序列化对应的内容
        return index - beginIndex;
    }
}

public class Player : BaseData
{
    public int atk;
    public override int GetBytesNum()
    {
        return 4;
    }

    public override byte[] Writing()
    {
        int index = 0;
        byte[] bytes = new byte[GetBytesNum()];
        WriteInt(bytes, atk, ref index);
        return bytes;
    }

    public override int Reading(byte[] bytes, int beginIndex = 0)
    {
        int index = beginIndex;
        atk = ReadInt(bytes, ref index);
        return index - beginIndex;
    }
}


public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestInfo info = new TestInfo();
        info.lev = 87;
        info.p = new Player();
        info.p.atk = 77;
        info.hp = 100;
        info.name = "唐老狮";
        info.sex = false;

        byte[] bytes = info.Writing();

        TestInfo info2 = new TestInfo();
        info2.Reading(bytes);
        print(info2.lev);
        print(info2.p.atk);
        print(info2.hp);
        print(info2.name);
        print(info2.sex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
