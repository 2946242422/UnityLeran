using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson103 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //1.亮度计算规则
        //  对图像的每个像素颜色进行乘法运算
        //  最终颜色 = 原始颜色 * 亮度变量

        //2.饱和度计算规则
        //  相对于灰度颜色进行插值
        //  第一步：灰度值（亮度）L = 0.2126*R + 0.7152*G + 0.0722*B
        //  第二步：灰度颜色 = (L, L, L)
        //  第三步：最终颜色 = lerp( 灰度颜色, 原始颜色, 饱和度变量 )

        //3.对比度计算规则
        //  相对于中性灰色进行插值
        //  第一步：中性灰颜色 = (0.5,0.5,0.5)
        //  第二步：最终颜色 = lerp( 中性灰色, 原始颜色, 对比度变量 )
        #endregion

        #region 知识点一 实现亮度、饱和度、对比度屏幕后期处理效果对应 Shader
        //1.新建一个Shader，名为BrightnessSaturationContrast（亮度饱和度对比度）
        //  删除其中无用代码
        //2.声明变量，并进行属性映射
        //  主纹理 _MainTex 2D
        //  亮度 _Brightness Float
        //  饱和度 _Saturation Float
        //  对比度 _Contrast Float
        //3.设置深度测试、剔除、深度写入
        //  ZTest Always  开启深度测试
        //  Cull Off    关闭剔除
        //  Zwrite Off  关闭深度写入
        //  这样的设置是屏幕后处理的标配

        //  因为屏幕后处理效果相当于在场景上绘制了一个与屏幕同宽高的四边形面片
        //  这样做的目的是避免它"挡住"后面的渲染物体
        //  比如我们在OnRenderImage前加入[ImageEffectOpaque]特性时
        //  透明物体会晚于该该屏幕后处理效果渲染，如果不关闭深度写入会影响后面的透明相关Pass
        //4.结构体相关
        //  顶点、纹理坐标
        //5.顶点着色器
        //  顶点转裁剪空间，uv缩放偏移
        //6.片元着色器
        //  对主纹理进行采样
        //  分别利用公式计算 亮度、饱和度、对比度
        //  返回处理后的颜色
        #endregion

        #region 知识点二 实现亮度、饱和度、对比度屏幕后期处理效果对应 C#代码
        //1.创建C#脚本，名为BrightnessSaturationContrast（亮度饱和度对比度）
        //2.继承屏幕后处理基类PostEffectBase
        //3.声明亮度饱和度对比度变量，用于控制效果变化
        //4.重写OnRenderImage方法，在其中设置材质球对应属性
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
