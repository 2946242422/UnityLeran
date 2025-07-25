using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson34 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 什么是协议生成工具？
        //协议生成工具，一般指 消息（协议）生成工具
        //就是专门用于自动化生成消息的程序

        //我们之前学习了：
        //1.消息的序列化和反序列化
        //2.区分消息类型
        //3.分包黏包
        //等等关于消息的相关知识

        //当需要一个新消息时，我们需要手动的按照规则去声明新的类
        //这部分工作费时又费力，技术含量也不高
        //如果前后端是统一的语言，我们按照语法声明一次就行
        //但是如果前后端语言不统一，比如前端用C#，后端用C++或者Java
        //那么前后端分开去声明，也容易造成沟通不一致，声明不统一的问题
        //所以如果靠我们手动的去声明消息类
        //是一件费时、费力、还容易出问题的事情

        //所以我们在商业游戏开发时，往往就需要使用协程生成工具
        //来帮助我们自动化的声明消息类
        //这样做的好处是：
        //1.提升开发效率
        //2.降低沟通成本,避免前后端消息不匹配的问题
        #endregion

        #region 知识点二 如何制作协议生成工具
        //要制作工具，首先要确定需求
        //对于协议生成工具来说，主要需求如下
        //1.通过配置文件配置消息或数据类 名字、变量等
        //2.工具根据该配置文件信息动态的生成 类文件（脚本文件，代码是自动生成的）
        //3.我们可以在开发中直接使用生成文件中声明好的消息和数据结构类进行开发

        //根据需求分析，我们需要做
        //1.确定协议配置方式
        //  可以使用json、xml、自定义格式进行协议配置
        //  主要目的，是通过配置文件确定
        //  1:消息或者数据结构类名字
        //  2:字段名等

        //2.确定生成格式
        //  最终我们是要自动生成类声明文件
        //  所以具体类应该如何生成需要确定格式
        //  比如：
        //  继承关系 固定写法
        //  序列化、反序列化 固定写法
        //  提取出共同点

        //3.制作生成工具
        //  基于配置文件 和 生成格式 动态的生成对应类文件
        #endregion

        #region 总结
        //制作协议生成工具的目的是一劳永逸
        //制作完成后
        //它可以提升开发效率，避免协议不统一等问题
        //之后多了不同的语言，按照规则进行编写即可
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
