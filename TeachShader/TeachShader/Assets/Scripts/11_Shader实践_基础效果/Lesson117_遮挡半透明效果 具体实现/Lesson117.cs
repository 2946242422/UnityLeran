using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson117 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 基本原理
        //两个Pass渲染对象，一个Pass用于渲染被遮挡部分，一个Pass用于渲染未遮挡模型。
        //被遮挡部分通过修改深度测试规则为大于并且关闭深度写入来达到目的

        //Schlick 菲涅耳近似等式
        //R(θ) = R0 + (1− R0 )(1 − V·𝑵)^𝟓 
        //其中：
        //R(θ) 表示入射角为θ时时的反射率
        //R0 是垂直入射某介质时的反射率
        //V 是视角方向单位向量（入射角）
        //N 是顶点法线单位向量
        #endregion

        #region 知识点一 遮挡半透明效果的具体实现 —— 实现基础半透明效果
        //1.新建Shader 取名 OcclusionTransparent1
        //  删除无用改代码
        //2.声明属性 映射属性
        //  主纹理
        //  透明度
        //3.复制一个Pass
        //4.第一个Pass
        //  4-1.关键点 深度测试改为大于 关闭深度写入 传统透明混合因子
        //      ZTest Greater
        //      ZWrite Off
        //      Blend SrcAlpha OneMinusSrcAlpha => 混合后的颜色 = (源颜色*源Alpha)+(目标颜色*(1−源Alpha))
        //  4-2.传统纹理采样实现，在片元着色器返回颜色中透明通道用透明度变量
        //5.第二个Pass
        //  传统纹理采样实现
        #endregion

        #region 知识点二 遮挡半透明效果的具体实现 —— 实现X射线半透明效果
        //1.新建Shader 取名 OcclusionTransparent2
        //  删除无用改代码
        //2.声明属性 映射属性
        //  主纹理
        //  自定义颜色
        //  菲涅尔反射率
        //  菲涅尔n次方
        //3.复制一个Pass
        //4.第一个Pass
        //  4-1.关键点 深度测试改为大于 关闭深度写入 传统透明混合因子
        //      ZTest Greater
        //      ZWrite Off
        //      Blend SrcAlpha OneMinusSrcAlpha => 混合后的颜色 = (源颜色*源Alpha)+(目标颜色*(1−源Alpha))
        //  4-2.结构体中
        //      顶点、世界空间法线、世界空间视角方向
        //  4-3.顶点着色器
        //      顶点变换、得到世界空间法线、世界空间视角方向
        //  4-4.片元着色器
        //      利用菲涅尔公式带入自定义参数计算，将得到的值作为自定义颜色的透明通道值
        //5.第二个Pass
        //  传统纹理采样实现
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
