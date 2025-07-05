using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson111 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 法线纹理的使用
        //第一步：在C#中设置摄像机的深度纹理模式
        //生成深度+法线纹理
        //Camera.main.depthTextureMode = DepthTextureMode.DepthNormals;

        //第二步：在Shader代码中按规则声明属性
        //sampler2D _CameraDepthNormalsTexture;

        //法线纹理使用
        //用于存储深度值的变量
        //float depth;
        //用于存储法线的变量
        //float3 normals;

        //对深度+法线纹理进行采样（其中xy是法线信息，zw是深度信息）
        //float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);

        //UnityCG.cginc 内置文件中的方法 用于得到深度值(0~1)和法线信息(观察空间下)
        //相当于一次性处理深度和法线
        //  DecodeDepthNormal(depthNormal, depth, normals);
        //单独得到深度
        //  depth = DecodeFloatRG(depthNormal.zw);
        //单独得到法线
        //  normals = DecodeViewNormalStereo(depthNormal);
        #endregion

        #region 知识点一 如何查看法线纹理信息
        //我们可以在屏幕后期处理中用我们学习过的获取法线信息的知识点
        //将法线作为颜色显示在屏幕上
        //感受法线纹理中存储的内容
        #endregion

        #region 知识点二 实现 查看法线纹理屏幕后期处理效果 对应 Shader
        //1.新建Shader，删除无用代码
        //2.声明变量
        //  在Shader中通过声明_CameraDepthNormalsTexture获取深度+法线纹理
        //3.顶点着色器
        //  只需要修改传入结构体类型
        //4.片元着色器
        //  对深度+法线纹理进行采样（其中xy是法线信息，zw是深度信息）
        //  float4 depthNormal = tex2D(_CameraDepthNormalsTexture, i.uv);
        //  UnityCG.cginc 内置文件中的方法 用于得到深度值(0~1)和法线信息(观察空间下)
        //  相当于一次性处理深度和法线
        //  用于存储深度值的变量
        //  float depth;
        //  用于存储法线的变量
        //  float3 normals;
        //  DecodeDepthNormal(depthNormal, depth, normals);
        //  单独得到深度
        //      depth = DecodeFloatRG(depthNormal.zw);
        //  单独得到法线
        //      normals = DecodeViewNormalStereo(depthNormal);
        //5.Fallback Off
        #endregion

        #region 知识点三 实现 查看法线纹理屏幕后期处理效果 对应 C#代码
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
