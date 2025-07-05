
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson120 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 
        //卡通风格渲染 基本原理
        //让光的过渡效果变硬并且实现轮廓描边！

        //关键点：
        //1.如何让光的过渡效果变硬
        //  利用渐变纹理处理漫反射，利用自定义简化公式计算高光反射
        //2.如何实现轮廓描边
        //  用两个Pass，一个渲染背面实现轮廓描边，一个正常渲染正面
        //  不需要关闭深度写入，模型正面的内容能够通过深度测试
        #endregion

        #region 知识点一 实现漫反射变硬和外轮廓效果
        //1.新建Shader 将渐变纹理的综合实践相关代码直接复制过来
        //2.将复制过来的代码的Pass 改为只渲染背面
        //3.加入阴影接收相关内容
        //  2-1.编译指令 #pragma multi_compile_fwdbase
        //  2-2.内置文件 #include "AutoLight.cginc"
        //  2-3.v2f结构体中加入 float3 worldPos 和 SHADOW_COORDS(4)
        //  2-4.顶点着色器计算中加入
        //      世界空间顶点坐标计算
        //      TRANSFER_SHADOW(o);
        //  2-5.光照衰减相关计算
        //      UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
        //      用来和halfLambertNum进行乘法运算
        //  2-6.FallBack "Diffuse"

        //4.处理渲染外轮廓（描边）
        //  4-1.加入边缘线颜色和宽度属性 _OutLineColor _OutLineWidth
        //  4-2.加入背面渲染的Pass，用于处理轮廓描边
        #endregion

        #region 知识点二 实现高光变硬效果
        //主要修改高光反射颜色计算相关内容
        //1.计算出半角向量
        //2.用法线和半角向量进行点乘
        //3.用点乘的结果和一个阈值进行比较 如果小于阈值 取0 大于阈值 取1
        //4.利用这个结果和高光颜色进行叠加
        //5.最后参与到布林方颜色公式计算中
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
