﻿using System;

namespace Lesson1_类和对象
{
    #region 知识回顾 面向对象概念回顾
    //万物皆对象
    //用程序来抽象（形容）对象
    //用面向对象的思想来编程
    #endregion

    #region 知识点一 什么是类
    //基本概念
    // 具有相同特征
    // 具有相同行为
    // 一类事物的抽象
    // 类是对象的模板
    // 可以通过类创建出对象
    // 类的关键词
    // class
    #endregion

    #region 知识点二 类申明在哪里
    //类一般申明在namespace语句块中
    #endregion

    #region 知识点三 类申明的语法
    class 类名
    {
        //特征——成员变量
        //行为——成员方法
        //保护特征——成员属性

        //构造函数和析构函数
        //索引器
        //运算符重载
        //静态成员
    }
    #endregion

    #region 知识点四 类申明实例
    //这个类是用来形容人类的
    //命名：用帕斯卡命名法 
    //注意：同一个语句块中的不同类 不能重名
    class Person
    {
        //特征——成员变量
        //行为——成员方法
        //保护特征——成员属性

        //构造函数和析构函数
        //索引器
        //运算符重载
        //静态成员
    }
    //这个类用来表示机器
    class Machine
    {
        //特征——成员变量
        //行为——成员方法
        //保护特征——成员属性

        //构造函数和析构函数
        //索引器
        //运算符重载
        //静态成员
    }

    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("类和对象");

            #region 知识点五 什么是（类）对象
            //基本概念
            // 类的申明 和 类对象(变量)申明是两个概念  
            // 类的申明 类似 枚举 和 结构体的申明  类的申明相当于申明了一个自定义变量类型
            // 而对象 是类创建出来的 
            // 相当于申明一个指定类的变量
            // 类创建对象的过程 一般称为实例化对象
            // 类对象 都是引用类型的
            #endregion

            #region 知识点六 实例化对象基本语法
            //类名 变量名;
            //类名 变量名 = null; (null代表空)
            //类名 变量名 = new 类名();
            #endregion

            #region 知识点七 实例化对象 
            Person p;
            Person p2 = null;//null 代表空 不分配堆内存空间
            Person p3 = new Person();//相当于一个人对象
            Person p4 = new Person();//相当于又是一个人对象
            //注意
            //虽然他们是来自一个类的实例化对象
            //但是他们的 特征 行为等等信息 都是他们独有的
            //千万千万 不要觉得他们是共享了数据 两个人 你是你 我是我 彼此没有关系

            Machine m = new Machine();
            Machine m1 = new Machine();

            //面向对象编程 就是开启了 女娲模式 造物模式 想要什么对象 就new什么对象
            //一切的对象  都是由我们来控制的 
            //我们相当于是整个程序世界的 总导演
            #endregion

        }
    }

    //总结
    // 类的申明 和 类对象的申明时两个概念
    // 类的申明 是申明对象的模板 用来抽象（形容）显示事物的
    // 类对象的申明 是用来表示现实中的 对象个体的

    // 类是一个自定义的变量类型
    // 实例化一个类对象 是在申明变量
}
