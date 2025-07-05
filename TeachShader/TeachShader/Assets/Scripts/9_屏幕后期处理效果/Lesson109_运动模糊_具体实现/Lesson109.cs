using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson109 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 运动模糊基本原理
        //保存之前的渲染结果，不断把当前的渲染图像叠加到之前的渲染图像中
        //通过RenderTexture来进行保存，用2个Pass来进行混合叠加

        //一个Pass混合RGB通道，由两张图片根据模糊程度决定最终混合效果
        //Blend SrcAlpha OneMinusSrcAlpha（(源颜色 * SrcAlpha) + (目标颜色 * (1 - SrcAlpha))）
        //ColorMask RGB （只改变颜色缓冲区中的RGB通道）

        //一个Pass混合A通道，由当前屏幕图像的A通道来决定
        //Blend One Zero（最终颜色 = (源颜色 * 1) + (目标颜色 * 0)）
        //ColorMask A（只改变颜色缓冲区中的A通道）
        #endregion

        #region 准备工作 搭建测试场景

        #endregion

        #region 知识点一 实现 运动模糊屏幕后期处理效果 对应 Shader
        //1.新建Shader 名为运动模糊(MotionBlur) 删除其中无用代码
        //2.属性声明
        //  主纹理 _MainTex
        //  模糊程度 _BlurAmount
        //3.共享CG代码 CGINCLUDE...ENDCG
        //  内置文件UnityCG.cginc引用
        //  属性映射
        //  结构体（顶点和UV)
        //  顶点着色器（裁剪空间转换 uv坐标赋值）
        //4.屏幕后处理效果标配
        //  ZTest Always
        //  Cull Off
        //  ZWrite Off
        //5.第一个Pass（用于混合RGB通道）
        //  混合因子 和 颜色蒙版设置
        //      Blend SrcAlpha OneMinusSrcAlpha（(源颜色 * SrcAlpha) + (目标颜色 * (1 - SrcAlpha))）
        //      ColorMask RGB （只改变颜色缓冲区中的RGB通道）
        //  片元着色器
        //      对主纹理采样后利用模糊程度作为A通道与颜色缓冲区颜色进行混合
        //6.第二个Pass（用户混合A通道）
        //  混合因子 和 颜色蒙版设置
        //      Blend One Zero（最终颜色 = (源颜色 * 1) + (目标颜色 * 0)）
        //      ColorMask A（只改变颜色缓冲区中的A通道）
        //  片元着色器
        //      对主纹理采样
        //7.FallBack Off
        #endregion

        #region 知识点二 实现 运动模糊屏幕后期处理效果 对应 C#
        //1.创建C#脚本，名为运动模糊MotionBlur
        //2.继承屏幕后处理基类PostEffectBase
        //3.声明成员属性
        //  公共的模糊程度
        //  私有的堆积纹理 accumulation Texture（用于存储上一次渲染结果）
        //4.重写OnRenderImage函数
        //  4-1.若堆积纹理为空或宽高变化 则初始化渲染纹理
        //      设置其hideFlags为HideFlags.HideAndDontSave（让其不保存）
        //  4-2.设置模糊程度属性
        //  4-3.将源纹理利用材质写入到堆积纹理中（相当于记录本次渲染结果）
        //  4-4.将堆积纹理写入目标纹理中
        //5.组件失活是销毁堆积纹理
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
