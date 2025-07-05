using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson110 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 深度纹理的使用
        //第一步：在C#中设置摄像机的深度纹理模式
        //生成深度纹理
        //Camera.main.depthTextureMode = DepthTextureMode.Depth;

        //第二步：在Shader代码中按规则声明属性
        //sampler2D _CameraDepthTexture;

        //深度纹理使用
        //使用深度纹理采样宏 得到的结果是非线性的
        //  float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
        //将非线性的深度值 转换到观察空间下 得到的值是像素点到摄像机的距离（线性深度值）
        //  float viewDepth = LinearEyeDepth(depth);
        //将非线性的深度值 转换到观察空间下 并将像素点到摄像机的距离转换到[0,1]区间内 （线性深度值）
        //  float linearDepth = Linear01Depth(depth);
        #endregion

        #region 知识点一 如何查看深度纹理信息
        //我们可以在屏幕后期处理中用我们学习过的获取深度信息的知识点
        //将深度值作为颜色的RGB值显示在屏幕上
        //感受深度纹理中存储的内容
        //理论上来说，如果深度值使用0~1范围的线性值
        //越接近近裁剪面越靠近黑色
        //越接近远裁剪面越靠近白色
        #endregion

        #region 知识点二 实现 查看深度纹理屏幕后期处理效果 对应 Shader
        //1.新建Shader，删除无用代码
        //2.声明变量
        //  在Shader中通过声明_CameraDepthTexture获取深度纹理
        //3.顶点着色器
        //  只需要修改传入结构体类型
        //4.片元着色器
        //  使用深度纹理采样宏 得到的结果是非线性的
        //  float depth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
        //  将非线性的深度值 转换到观察空间下 并将像素点到摄像机的距离转换到[0,1]区间内 （线性深度值）
        //  float linearDepth = Linear01Depth(depth);
        //  将深度值作为返回颜色的RGB值
        //5.Fallback Off
        #endregion

        #region 知识点三 实现 查看深度纹理屏幕后期处理效果 对应 C#代码
        //1.新建C#脚本，命名和Shader相同
        //2.继承PostEffectBase
        //3.在Start函数中设置深度纹理模式
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
