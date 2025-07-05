using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson112 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾
        //利用深度纹理实现运动模糊效果 的基本原理是
        //得到像素当前帧和上一帧中在裁剪空间下的位置，
        //利用两个位置计算出物体的运动方向，从而模拟出运动模糊的效果
        #endregion

        #region 补充知识点
        //我们需要通过C#代码为Shader设置矩阵变量
        //但是ShaderLab语法中的属性中并没有矩阵类型的变量
        //因此我们只需要在CG语句中声明矩阵属性即可
        //这样C#中通过矩阵的属性名同样可以进行设置
        //举例：
        //CG代码中声明4x4矩阵 —— float4x4 _ClipToWorldMatrix;
        //C#代码中声明4x4矩阵 —— Matrix4x4 frontClipToWorldMatrix;
        //通过材质球指明变量进行设置即可
        //material.SetMatrix("_ClipToWorldMatrix", frontClipToWorldMatrix);
        #endregion

        #region 知识点一 实现 利用深度纹理实现运动模糊屏幕后期处理效果 对应 Shader
        //1.新建Shader文件，取名 MotionBlurWithDepthTexture 深度纹理运动模糊效果
        //2.声明属性，进行属性映射
        //  主纹理 _MainTex
        //  模糊偏移量 _BlurSize

        //  深度纹理 _CameraDepthTexture
        //  当前帧裁剪到世界空间变换矩阵 float4x4 _ClipToWorldMatrix
        //  上一帧世界到裁剪空间变换矩阵 float4x4 _FrontWorldToClipMatrix
        //3.屏幕后处理标配
        //  ZTest Always 
        //  Cull Off
        //  ZWrite Off
        //4.结构体
        //  顶点和uv坐标
        //5.顶点着色器
        //  坐标转换 uv坐标赋值
        //6.片元着色器
        //  6-1:得到裁剪空间下的两个点
        //      得到点一
        //      深度值获取
        //      构建裁剪空间下组合坐标 uv 和 深度
        //      得到点二
        //      裁剪空间坐标转世界空间(注意进行齐次除法）
        //      利用上一帧变换矩阵将世界空间坐标转裁剪空间（注意进行齐次除法）
        //  6-2:得到运动方向
        //      用当前帧点-上一帧点 得到运动方向
        //  6-3:进行模糊处理
        //      利用模糊偏移量变量进行3次偏移采样颜色后进行平均值计算
        //7.FallBack Off
        #endregion

        #region 知识点二 实现 利用深度纹理实现运动模糊屏幕后期处理效果 对应 C#
        //1.创建C#代码，命名和Shader一样
        //2.继承屏幕后处理基类PostEffectBase
        //3.声明模糊偏移量变量和用于记录上一次变换矩阵的变量
        //4.重写OnRenderImage函数
        //  在其中进行属性设置，变换矩阵计算，屏幕后处理
        //5.在生命周期函数中启用深度纹理，初始化上一帧变换矩阵
        #endregion

        #region 知识点三 其他注意点
        //1.考虑不同平台可能存在的垂直翻转问题、
        //  #if UNITY_UV_STARTS_AT_TOP
        //  if (_MainTex_TexelSize.y < 0)
        //    o.uv_depth.y = 1 - o.uv_depth.y;
        //  #endif

        //2.让移动方向向量除以2
        //  从而降低运动模糊效果的强度，不要过于强烈
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
