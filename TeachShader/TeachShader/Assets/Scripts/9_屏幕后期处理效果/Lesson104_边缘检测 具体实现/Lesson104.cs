using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson104 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //1.灰度值 L = 0.2126*R + 0.7152*G + 0.0722*B
        //2.边缘检测效果的基本原理
        //  得到 当前像素以及其 上下左右、左上左下、右上右下共9个像素的灰度值
        //  用这9个灰度值和 Sobel算子 进行卷积计算得到梯度值 G = abs(Gx) + abs(Gy)
        //  最终颜色 = lerp（原始颜色，描边颜色，梯度值）
        //3.如何得到当前像素周围8个像素位置
        //  利用 float4 纹理名_TexelSize 纹素 信息得到当前像素周围8个像素位置
        #endregion

        #region 准备工作
        //1.导入图片资源 设置为Sprite
        //2.新建场景
        //3.在场景中使用导入资源新建Sprite对象 将其填充满Game窗口 用于测试
        #endregion

        #region 知识点一 实现边缘检测屏幕后期处理效果对应 Shader
        //1.新建Shader，取名边缘检测EdgeDetection，删除无用代码
        //2.声明属性，进行属性映射
        //  主纹理 _MainTex
        //  边缘描边用的颜色 _EdgeColor
        //  注意属性映射时 使用内置纹素变量 _MainTex_TexelSize
        //3.屏幕后处理效果标配
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //4.结构体相关
        //  顶点
        //  uv数组，用于存储9个像素点的uv坐标
        //5.顶点着色器
        //  顶点坐标转换
        //  用uv数组装载9个像素uv坐标
        //6.片元着色器
        //  利用卷积获取梯度值（可以声明一个Sobel算子计算函数和一个灰度值计算函数）
        //  利用梯度值在原始颜色和边缘颜色之间进行插值得到最终颜色
        //7.FallBack Off
        #endregion

        #region 知识点二 实现边缘检测屏幕后期处理效果对应 C#代码
        //1.创建C#脚本，名为EdgeDetection（边缘检测）
        //2.继承屏幕后处理基类PostEffectBase
        //3.声明边缘颜色变量，用于控制效果变化
        //4.重写UpdateProperty方法，设置材质球属性
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
