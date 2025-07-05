using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson135 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识点一 回顾表面着色器基本原理
        //我们在Lesson10中已经对表面着色器有一定的认识
        //我们先来回顾下相关知识

        //我们一般会使用以下3种形式来编写Unity Shader
        //1.表面着色器（可控性较低）
        //2.顶点/片元着色器（重点学习）
        //3.固定函数着色器（基本已弃用，了解即可）

        //表面着色器（Surface Shader）是Unity自己创造的一种着色器代码类型
        //它的本质是对顶点/片元着色器的一层封装
        //它需要的代码量很少，很多工作都帮助我们去完成了
        //但是缺点是渲染的消耗较大，可控性较低
        //它的优点在于，它帮助我们处理了很多光照细节，我们可以直接使用而无需自己计算实现光照细节

        //我们可以在创建Shader时，选择创建Standard Surface Shader
        //通过观察该Shader文件的内部结构，你会发现
        //着色器相关代码被放在SubShader语句块中（并非Pass）的 CGPROGRAM 和 ENDCG 之间

        //表面着色器的特点就是
        //1.直接在SubShader语句块中书写着色器逻辑
        //2.我们不需要关心也不需要使用多个Pass，每个Pass如何渲染，Unity会在内部帮助我们去处理
        //3.可以使用CG或HLSL两种Shader语言去编写Shader逻辑
        //4.代码量较少，可控性较低，性能消耗较高
        //5.适用于处理需要和各种光源打交道的着色器（主机、PC平台时更适用，移动平台需要考虑性能消耗）

        //总的来说：
        //表面着色器实际上就是在顶点/片元着色器的基础上进行了一次封装
        //在处理光照、阴影、反射、折射等等效果时，简化了繁琐的计算流程
        //我们不需要自己去处理这些内容，Unity内部会自动帮助我们进行计算
        #endregion

        #region 知识点二 了解表面着色器的结构
        //我们通过事例对比表面着色器和顶点/片元着色器的区别
        //最大区别就是
        //表面着色器中没有显示的Pass了
        //而其中最为关键的几个部分为：
        //1.编译指令
        //2.结构体
        //3.自定义函数

        //当我们编写完表面着色器后，Unity会自动将其编译为顶点/片元着色器
        //会将光照处理、反射、折射、阴影、雾效等等相关计算代码自动加入
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
