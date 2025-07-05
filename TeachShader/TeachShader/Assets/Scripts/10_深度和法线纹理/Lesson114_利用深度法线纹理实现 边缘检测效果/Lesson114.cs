using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson114 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region 知识回顾 深度+法线纹理实现边缘检测 基本原理
        //基于Roberts(罗伯茨)交叉算子，通过比较对角线上的的像素的深度和法线值，判断是否在边缘上。
        //1.得到对角线上的像素
        //  利用纹素变量进行uv坐标偏移，得到对角线像素位置（可以加入一个采样偏移距离变量控制描边粗细）
        //2.进行深度和法线值的比较
        //  获取对角线像素的深度+法线信息，通过减法得到差值，用差值和阈值进行比较，判断两点是否在同一平面上
        //3.决定是否在边缘
        //  根据比较结果，决定中心像素使用的颜色是原本的颜色还是描边的颜色
        #endregion

        #region 知识点一 实现 利用深度+法线纹理实现边缘检测屏幕后期处理效果 对应 Shader
        //1.新建Shader，取名EdgeDetectionWithDepthNormalsTexture
        //  删除无用代码
        //2.声明属性，属性映射
        //  主纹理 _MainTex
        //  边缘检测强度 _EdgeOnly 0显示场景 1只显示边缘 用于控制自定义背景色程度
        //  描边颜色 _EdgeColor
        //  背景颜色 _BackgroundColor
        //  采样偏移距离 _SampleDistance
        //  深度敏感度 _SensitivityDepth
        //  法线敏感度 _SensitivityNormal
        //  注意：映射属性时需要加入
        //  纹素 _MainTex_TexelSize
        //  深度+法线纹理 _CameraDepthNormalsTexture
        //3.屏幕后处理标配
        //4.结构体
        //  顶点坐标
        //  uv数组，5个空间（存储中心点、对角线4个点）
        //5.顶点着色器
        //  4-1.顶点坐标转换
        //  4-2.5个uv坐标赋值，按这样的顺序
        //      中心点、左上角、右下角、右上角、左下角
        //6.片元着色器
        //  5-1.直接采样得到对角线四个点的深度法线信息
        //  5-2.实现一个用于比较两点深度、法线信息的函数，返回0或1，方便进行插值计算
        //  5-3.声明一个插值变量 0代表使用边缘色 1代表使用源颜色
        //  5-4.考虑背景色差值
        //7.FallBack Off
        #endregion

        #region 知识点二 实现 利用深度+法线纹理实现边缘检测屏幕后期处理效果 对应 C#
        //1.新建C#脚本，取名和Shader相同
        //2.继承PostEffectBase
        //3.声明可控变量
        //4.Start函数中 开启深度+法线纹理
        //  注意：不要直接=，要|=，避免关闭深度纹理
        //5.重写UpdateProperty函数
        //  在其中设置属性即可
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
